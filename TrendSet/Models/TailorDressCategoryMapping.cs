using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace TrendSet.Models
{
    public class TailorDressCategoryMapping
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please fill mandatory field")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Please fill mandatory field")]
        public int DressTypeId { get; set; }

        public int UserId { get; set; }

        

        public int Cost { get; set; }



        public virtual Category Category { get; set; }
        public virtual DressType DressType { get; set; }
      

        public virtual UserDetail UserDetail { get; set; }

        public ICollection<Order> Order { get; set; }

    }
}