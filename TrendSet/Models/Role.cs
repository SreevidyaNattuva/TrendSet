using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace TrendSet.Models
{
    public class Role
    {
        [Key]
        [Required(ErrorMessage = "Please enter the required field")]
        public int RoleId { get; set; }

        [Display(Name ="Role Name")]
        [Required(ErrorMessage = "Please enter the required field")]
        public string RoleName { get; set; }

        public ICollection<RoleLoginMapping> RoleLoginMapping { get; set; }
    }
}