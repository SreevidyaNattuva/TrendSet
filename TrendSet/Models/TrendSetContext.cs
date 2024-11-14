using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace TrendSet.Models
{
    public class TrendSetContext : DbContext
    {
        public TrendSetContext(): base("TrendSet") { }

        public DbSet<UserDetail> UserDetails { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<RoleLoginMapping> RoleLoginMappings { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<DressType> DressTypes { get; set; }

        public DbSet<DressTypeCategoryMapping> DressTypeCategoryMappings { get; set; }

        

        public DbSet<TailorDressCategoryMapping> TailorDressCategoryMappings { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OnlinePayment> OnlinePayments { get; set; }

    }
}