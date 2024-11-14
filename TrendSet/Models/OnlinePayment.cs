using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TrendSet.Models
{
    public class OnlinePayment
    {
        [Key]
        public int PaymentId { get; set; }
        [Required(ErrorMessage = "Please fill mandatory field")]
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "Please fill mandatory field")]
        public string ExpireDate { get; set; }
        [Required(ErrorMessage = "Please fill mandatory field")]
        public string CVV { get; set; }
    }
}