using System;
using System.Collections.Generic;
using RealRMS.Models;
using RealRMS.Utility;

namespace RealRMS.Repository
{
    public class RolesRepository : Repository<Role> {
        public const string ROLES_GET_ALL = "roles_getAll";
        public const string ROLES_GETFORUSER = "roles_getForUser";

        public RolesRepository(IConnectionFactory conn = null, ISqlWorker broker = null) : base(conn) {}


        public override bool Create(Role model) {
            throw new NotImplementedException();
        }

        public override bool Update(Role model) {
            throw new NotImplementedException();
        }

        public override Role Get(int id) {
            throw new NotImplementedException();
        }

        public override IEnumerable<Role> GetAll() {
            broker.SetupCommand(ROLES_GET_ALL);
            return broker.GetObject<Role>();
        }

        public IEnumerable<Role> GetForUser(int userId) {
            IEnumerable<Role> roles = new List<Role>();
            if (userId == default(int))
                return roles;
            broker.SetupCommand(ROLES_GETFORUSER);
            broker.AddInputParameter("@idUser", userId);
            return broker.GetObject<Role>();
        }
    }
}