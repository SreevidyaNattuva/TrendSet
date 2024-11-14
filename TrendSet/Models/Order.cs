using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TrendSet.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string TopMaterialType { get; set; }
        public double? TopLengths { get; set; }
        public double? Neck { get; set; }
        public double? TopWaist { get; set; }
        public double? Chest { get; set; }
        public double? ShoulderLength { get; set; }
        public string BottomMaterialType { get; set; }
        public double? BottomLength { get; set; }
        public double? Hip { get; set; }
        public double? KneeLength { get; set; }
        [Required(ErrorMessage = "Please fill mandatory field")]
        [DataType(DataType.Date)]
        public DateTime? ExpectedDate { get; set; }
        public int? Bill { get; set; }
        [Required(ErrorMessage = "Please fill mandatory field")]
        public string Courier { get; set; }
        public string Status { get; set; }
        public string BillStatus { get; set; }
        public virtual TailorDressCategoryMapping TailorDressCategoryMapping { get; set; }

    }
}