using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EF.EntityTypeConfigurations
{
    class RepostConfiguration : IEntityTypeConfiguration<Repost>
    {
        public void Configure(EntityTypeBuilder<Repost> builder)
        {
            builder.HasKey(rep => rep.Id);

            builder.HasOne(rep => rep.Request).WithMany(req => req.Reposts).HasForeignKey(rep => rep.RequestId).OnDelete(DeleteBehavior.Cascade);
            //builder.HasOne(rep => rep.UserProfile).WithMany(user => user.Reposts).HasForeignKey(rep => rep.UserProfileId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
