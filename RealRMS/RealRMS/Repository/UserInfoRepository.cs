using System.Collections.Generic;
using System.Data;
using System.Linq;
using RealRMS.Models;
using RealRMS.Security;
using RealRMS.Utility;
using RealRMS.Utility.Extension;

namespace RealRMS.Repository {
    public class UserInfoRepository : Repository<UserInfoModel> {
        private const int SALT_SIZE = 20;

        public const string USER_INSERT = "user_insert";
        public const string USER_ROLE_INSERT = "user_roles_insert";
        public const string USER_ROLE_UPDATE = "user_roles_update";
        public const string USER_UPDATE = "user_update";
        public const string USER_GET = "user_get";
        public const string USER_GET_ALL = "user_getAll";
        public UserInfoRepository(IConnectionFactory conn = null, ISqlWorker broker = null) : base(conn) {}

        #region CRUD Operation

        public override bool Create(UserInfoModel model) {
            int? uId;
            string salt = string.Empty;
            string hash = string.Empty;

            if (model.Password.Length > 0) {
                salt = CreateSalt(SALT_SIZE);
                hash = GenerateHash(model.ConfirmPassword, salt);
            }

            broker.SetupCommand(USER_INSERT);
            broker.AddInputParameter("@firstName", model.FirstName);
            broker.AddInputParameter("@lastName", model.LastName);
            broker.AddInputParameter("@middleName", model.MiddleName);
            broker.AddInputParameter("@password", hash);
            broker.AddInputParameter("@salt", salt);
            broker.AddInputParameter("@dob", model.DateOfBirth);
            broker.AddInputParameter("@ssn", model.Ssn);
            broker.AddInputParameter("@street", model.Street);
            broker.AddInputParameter("@city", model.City);
            broker.AddInputParameter("@state", model.State);
            broker.AddInputParameter("@zip", model.Zip);
            broker.AddInputParameter("@phone", model.Phone);
            broker.AddInputParameter("@email", model.Email);
            broker.AddInputParameter("@rate", model.Rate);

            uId = broker.ExecuteScalar().SafeToInt();

            if (model.Roles.Any()) {
                broker.SetupCommand(USER_ROLE_INSERT);
                broker.AddInputParameter("@roleList", CreateDataTableForPassingTvp<int>(model.Roles.Select(r => r.Id)), SqlCollectionType.IntegerList);
                broker.AddInputParameter("@idUser", uId);
                broker.ExecuteNonQuery();
            }

            model.Id = uId.Value;
            return uId > 0;
        }

        public override bool Update(UserInfoModel model) {
            broker.SetupCommand(USER_UPDATE);
            broker.AddInputParameter("@id", model.Id);
            broker.AddInputParameter("@firstName", model.FirstName);
            broker.AddInputParameter("@lastName", model.LastName);
            broker.AddInputParameter("@middleName", model.MiddleName);
            broker.AddInputParameter("@dob", model.DateOfBirth);
            broker.AddInputParameter("@ssn", model.Ssn);
            broker.AddInputParameter("@street", model.Street);
            broker.AddInputParameter("@city", model.City);
            broker.AddInputParameter("@state", model.State);
            broker.AddInputParameter("@zip", model.Zip);
            broker.AddInputParameter("@phone", model.Phone);
            broker.AddInputParameter("@email", model.Email);
            broker.AddInputParameter("@rate", model.Rate);
            broker.ExecuteNonQuery();

            broker.SetupCommand(USER_ROLE_UPDATE);
            broker.AddInputParameter("@roleList", CreateDataTableForPassingTvp<int>(model.Roles.Select(r => r.Id)), SqlCollectionType.IntegerList);
            broker.AddInputParameter("@idUser", model.Id);
            broker.ExecuteNonQuery();

            return true;

        }

        
        public override UserInfoModel Get(int id) {
            broker.SetupCommand(USER_GET);
            broker.AddInputParameter("@id", id);
            UserInfoModel model = broker.GetObject<UserInfoModel>().FirstOrDefault();
            if (model == null)
                return model;
            RolesRepository repo = new RolesRepository();
            model.Roles = repo.GetForUser(model.Id);
            return model;
        }

        #endregion

        public bool Login(int id, string password) {
            UserInfoModel model = Get(id);
            bool success = false;
            if (model != null && model.Id == id)
                success = CheckPassword(model, password);
            return success;
        }


        public override IEnumerable<UserInfoModel> GetAll() {
            broker.SetupCommand(USER_GET_ALL);
            IEnumerable<UserInfoModel> model = broker.GetObject<UserInfoModel>();
            if (model == null)
                return new UserInfoModel[0];
            RolesRepository repo = new RolesRepository();

            foreach (var usr in model) {
                usr.Roles = repo.GetForUser(usr.Id);
            }
            return model;
        }

        private bool CheckPassword(UserInfoModel m, string password) {
            string hash = GenerateHash(password, m.Salt); // Perhaps this should only be exposed as a PRIVATE repository method
            return hash == m.Password;
        }

        private string CreateSalt(int size) {
            Encryption encryption = new Encryption();
            return encryption.GenerateSalt(size);
        }

        private string GenerateHash(string password, string salt) {
            Encryption encryption = new Encryption();
            return encryption.GenerateHash(salt, password);
        }

        private DataTable CreateDataTableForPassingTvp<T>(IEnumerable<T> values) {
            DataTable table = new DataTable();
            table.Columns.Add("value", typeof(T));
            foreach (var value in values) {
                table.Rows.Add(value);
            }
            return table;
        
        }
    }
}