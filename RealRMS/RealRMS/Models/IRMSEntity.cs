using System.Collections.Generic;

namespace RealRMS.Models {
    public interface IRMSEntity {
        int Id { get; set; }

    }

    public interface IRMSValidatableEntity : IRMSEntity {
        IEnumerable<ValidationError> ValidationErrors { get; set; }
    }
}