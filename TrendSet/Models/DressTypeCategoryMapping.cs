using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace TrendSet.Models
{
    public class DressTypeCategoryMapping
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the required field")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter the required field")]
        public int DressTypeId { get; set; }




        public virtual Category Category { get; set; }
        public virtual DressType DressType { get; set; }

    }
}