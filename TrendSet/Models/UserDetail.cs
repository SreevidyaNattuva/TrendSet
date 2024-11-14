using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TrendSet.Models
{
    public class UserDetail
    {
        [Key]

        public int UserId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please fill mandatory field")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please fill mandatory field")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Please fill mandatory field")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DoB { get; set; }

        [Required(ErrorMessage = "Please fill mandatory field")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please fill mandatory field")]
        [Display(Name = "Contact Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[[7-9]{1}[0-9]{9}$",
        ErrorMessage = "Not a valid phone number")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Please fill mandatory field")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please fill mandatory field")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please fill mandatory field")]
        public string Password { get; set; }



        public ICollection<RoleLoginMapping> RoleLoginMapping { get; set; }

        public virtual ICollection<TailorDressCategoryMapping> TailorDressCategoryMappings { get; set; }
    }
}