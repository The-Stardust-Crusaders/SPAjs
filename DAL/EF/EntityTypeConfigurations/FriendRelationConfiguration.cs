using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EF.EntityTypeConfigurations
{
    class FriendRelationConfiguration : IEntityTypeConfiguration<FriendRelation>
    {
        public void Configure(EntityTypeBuilder<FriendRelation> builder)
        {
            builder.HasKey(rel => rel.Id);

            builder.HasOne(rel => rel.Initiator).WithMany(user => user.FriendRelations).HasForeignKey(rel => rel.InitiatorId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
