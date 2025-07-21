using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JuniorBoardIT.Core.Entities;

namespace JuniorBoardIT.Core
{
    public class DataContext : IdentityDbContext<Users>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> User => Set<User>();
        public DbSet<Roles> AppRoles => Set<Roles>();
        public DbSet<JobOffers> JobOffers => Set<JobOffers>();
        public DbSet<Reports> Reports => Set<Reports>();


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
