using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RealRMS.Utility.Attributes;

namespace RealRMS.Models {
    public class MenuModel : IRMSValidatableEntity {
        public MenuModel() {
            MenuCategory = new MenuCategoryModel();
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [Range(.99, 100, ErrorMessage = "Item must have a valid price")]
        [Required]
        public double Price { get; set; }

        [Required]
        public MenuCategoryModel MenuCategory { get; set; }

        [NotInQuery]
        public IEnumerable<ValidationError> ValidationErrors { get; set; }
    }
}