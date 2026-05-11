using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;

namespace WebBanDienThoai.Models
{
    public class AppDbContext : DbContext
    {
        // Change "MyDB" to "AppDbContext" to match your Web.config
        public AppDbContext() : base("name=AppDbContext") { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<UserAccount> Users { get; set; }
    }
}
