using HelpyWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpyWeb.Data
{
    public class HelpyDbContext : DbContext
    {
        public HelpyDbContext(DbContextOptions<HelpyDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserRoleDetail> UserRoleDetails { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<UserBillsDetail> UserBillsDetails { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<UserImagesDetail> UserImagesDetails { get; set; }
    }
}
