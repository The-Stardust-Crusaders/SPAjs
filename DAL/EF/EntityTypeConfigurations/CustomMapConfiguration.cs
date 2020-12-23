using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EF.EntityTypeConfigurations
{
    class CustomMapConfiguration : IEntityTypeConfiguration<CustomMap>
    {
        public void Configure(EntityTypeBuilder<CustomMap> builder)
        {
            builder.HasKey(map => map.Id);

            builder.HasOne(map => map.Request).WithMany(req => req.Maps).HasForeignKey(map => map.RequestId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
