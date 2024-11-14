using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TrendSet.Models
{
    public class Category
    {
        [Key]
        [Display(Name ="Category Id")]
        [Required(ErrorMessage ="Please enter the required field")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Please enter the required field")]
        [Display(Name ="Category Name")]
        public string CategoryName { get; set; }

        public virtual ICollection<DressTypeCategoryMapping> DressTypeCategoryMappings { get; set; }

        public virtual ICollection<TailorDressCategoryMapping> TailorDressCategoryMappings { get; set; }
    }
}