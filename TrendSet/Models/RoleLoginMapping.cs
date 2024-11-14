using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TrendSet.Models
{
    public class RoleLoginMapping
    {
        [Key]

        public int Id { get; set; }

        
        public int UserId { get; set; }

        public UserDetail UserDetail { get; set; }

        
        public int RoleId { get; set; }

        public Role Role
        {
            get; set;
        }
    }
}