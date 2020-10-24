using System.Collections.Generic;

namespace RealRMS.Models {
    public class SiteSettings : IRMSEntity {
        public int Id { get; set; }

        public IList<string> IpAddresses { get; set; }

        public string SiteUrl { get; set; }

        public string DatabaseName { get; set; }
    }
}