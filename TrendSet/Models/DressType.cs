using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TrendSet.Models
{
    public class DressType
    {
        [Key]
        [Required(ErrorMessage = "Please enter the required field")]
        public int DressTypeId { get; set; }

        [Required(ErrorMessage = "Please enter the required field")]
        [Display(Name ="Dress Type")]
        public string DressTypeName { get; set; }

        public virtual ICollection<DressTypeCategoryMapping> DressTypeCategoryMappings { get; set; }

        public virtual ICollection<TailorDressCategoryMapping> TailorDressCategoryMappings { get; set; }
    }
}