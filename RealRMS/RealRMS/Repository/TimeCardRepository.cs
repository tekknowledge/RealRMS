using System;
using System.Collections.Generic;
using System.Linq;
using RealRMS.CacheKeyConstants;
using RealRMS.Models;
using RealRMS.Utility;
using RealRMS.Utility.Extension;

namespace RealRMS.Repository
{
    public class TimeCardRepository : RepositoryWithCache<TimeCardModel> {
        public const string TIMECARD_GETFOR_EMPLOYEE = "timecard_getForEmployee";
        public const string TIMECARD_UPDATE = "timecard_update";
        public const string TIMECARD_INSERT = "timecard_insert";

        public TimeCardRepository(ICacheWrapper cache, IConnectionFactory factory, ISqlWorker broker) : base(cache, factory, broker) {
            
        }

        public TimeCardRepository(ICacheWrapper cache) : base(cache) {
            
        }
        public override bool Create(TimeCardModel model) {
            broker.SetupCommand(TIMECARD_INSERT);

            broker.AddInputParameter("@EmployeeId", model.EmployeeId);
            model.Id = int.Parse(broker.ExecuteScalar().SafeToString());
            if (model.Id == (default(int)))
                return false;

            cache.Set(SessionCacheConstants.TIMECARD, model);
            return true;
        }

        public override bool Update(TimeCardModel model) {
            broker.SetupCommand(TIMECARD_UPDATE);

            // Alternatively, we could just call with Id
            broker.AddInputParameter("@Id", model.Id);

            bool isUpdated = broker.ExecuteNonQuery() > 0;
            if (isUpdated) {
                cache.Remove(SessionCacheConstants.TIMECARD);
                Get(model.EmployeeId);
            }
            return isUpdated;
        }

        public override TimeCardModel Get(int id) {
            TimeCardModel model = cache.Get(SessionCacheConstants.TIMECARD) as TimeCardModel;
            if (model == null) {
                broker.SetupCommand(TIMECARD_GETFOR_EMPLOYEE);
                broker.AddInputParameter("@EmployeeId", id); // Stored procedure gets most recent clock-in
                model = broker.GetObject<TimeCardModel>().FirstOrDefault();
            }
            // There are a few scenarios:

            // 1: The user does not have a clock-in record => return a fresh record
            if (model == null)
                return NewTimeCardEmpId(id);

            // 2: The user's last clock-in record is a completed record => return a fresh record
            if (model.In.HasValue && model.Out.HasValue)
                return NewTimeCardEmpId(id);

            // 3: The user has a NULL clock out date, but I'm sure they couldn't have worked more than 12 hours => return a fresh record...We can sort out their cheating later :)
            if (model.In.HasValue && !model.Out.HasValue && DateTime.Now.Subtract(model.In.Value).TotalHours >= 12) {
                return NewTimeCardEmpId(id);
            }

            SetCache(SessionCacheConstants.TIMECARD, model);

            // 4: The user is still clocked in from working their shift => return their last record
            return model;

        }

        public override IEnumerable<TimeCardModel> GetAll() {
            throw new NotImplementedException();
        }

        private TimeCardModel NewTimeCardEmpId(int empId) {
            var model = new TimeCardModel { EmployeeId = empId, In = null, Out = null };
            SetCache(SessionCacheConstants.TIMECARD, model);
            return model;
        }
    }
}