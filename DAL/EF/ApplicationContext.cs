using DAL.EF.EntityTypeConfigurations;
using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class ApplicationContext : IdentityDbContext<UserProfile>
    {
        public DbSet<Request> Requests { get; set; }
        public DbSet<CustomMap> Maps { get; set; }
        public DbSet<Repost> Reposts { get; set; }
        public DbSet<FriendRelation> FriendRelations { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserProfileConfiguration());
            builder.ApplyConfiguration(new RequestConfiguration());
            builder.ApplyConfiguration(new CustomMapConfiguration());
            builder.ApplyConfiguration(new RepostConfiguration());
            builder.ApplyConfiguration(new FriendRelationConfiguration());
        }
    }
}
