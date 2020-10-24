using System;
using System.Collections.Generic;
using System.Data;
using RealRMS.Models;
using RealRMS.Utility;
using RealRMS.Utility.Extension;

namespace RealRMS.Repository {
    public class SiteSettingsRepository : Repository<SiteSettings> {
        public SiteSettingsRepository(IConnectionFactory factory = null, ISqlWorker broker = null) : base(factory, broker) {
            
        }

        private const string SETTINGS_GET = "siteSettings_get";
        private const string IPS_GET = "siteSettings_allowedIps_get";
        private const string SETTINGS_UPDATE = "siteSettings_update";
        private const string IPS_INSERT = "siteSettings_allowedIps_insert";

        public override SiteSettings Get(int id) {
            SiteSettings settings = new SiteSettings();
            broker.SetupCommand(SETTINGS_GET);
            broker.AddInputParameter("id", id);
            using (IDataReader rdr = broker.GetDataReader()) {
                if (rdr.Read()) {
                    settings.Id = (int)rdr["id"];
                    settings.IpAddresses = new List<string>();
                    settings.DatabaseName = rdr["databaseName"].SafeToString();
                }
            }
            /*broker.SetupCommand(IPS_GET);
            broker.AddInputParameter("idSiteSettings", settings.Id);
            using (IDataReader rdr = broker.GetDataReader()) {
                while (rdr.Read()) {
                    settings.IpAddresses.Add(rdr["IpAddress"].SafeToString());
                }
            }*/
            return settings;
        }

        public override IEnumerable<SiteSettings> GetAll() {
            throw new NotImplementedException();
        }

        public override bool Create(SiteSettings model) {
            throw new NotImplementedException();
        }

        public override bool Update(SiteSettings model) {
            return true;
            /*IList<string> existing = Get(model.Id).IpAddresses;
            foreach (var address in model.IpAddresses) {
                if (!existing.Contains(address)) {
                    broker.SetupCommand(IPS_INSERT);
                    broker.AddInputParameter("@idSiteSettings", model.Id);
                    broker.AddInputParameter("@ipAddress", address);
                }
            }
            return broker.ExecuteNonQuery() > 0;*/

        }
    }
}