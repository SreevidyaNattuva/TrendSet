using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TrendSet.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please fill mandatory field")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please fill mandatory field")]
        public string Password { get; set; }
    }
}