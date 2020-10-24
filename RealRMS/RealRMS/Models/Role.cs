using RealRMS.Utility.Attributes;

namespace RealRMS.Models {
    public class Role : IRMSEntity {
        [DatabaseColumnName("Rolesid")]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}