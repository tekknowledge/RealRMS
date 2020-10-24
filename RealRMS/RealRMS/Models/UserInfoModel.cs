using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RealRMS.Utility.Attributes;

namespace RealRMS.Models
{
    public class UserInfoModel : IRMSValidatableEntity {
        private IEnumerable<Role> _roles = null;

        public UserInfoModel() {
            _roles = new List<Role>();
        }

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [NotInQuery]
        public string ConfirmPassword { get; set; }

        public string Salt { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DatabaseColumnName("dob")]
        public DateTime DateOfBirth { get; set; }

        [RegularExpression(@"^\d{3}-\d{2}-\d{4}$", ErrorMessage= "Invalid SSN")]
        public string Ssn { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip")]
        public string Zip { get; set; }

        [Required]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Invalid Phone")]
        public string Phone { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        [Range(3.0, 100.0, ErrorMessage = "Value must be greater than $3.00 for a server and greater than $7.85 for anyone else.")]
        public double Rate { get; set; }

        [NotInQuery]
        public IEnumerable<Role> Roles {
            get {
                return _roles;
            }
            set {
                _roles = value;
            }
        }

        [NotInQuery]
        public string RoleString {
            get; set;
        }

        [NotInQuery]
        public IEnumerable<ValidationError> ValidationErrors { get; set; }
    }
}