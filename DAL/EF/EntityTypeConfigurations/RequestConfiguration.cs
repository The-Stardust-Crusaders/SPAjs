using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EF.EntityTypeConfigurations
{
    class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.HasKey(req => req.Id);

            builder.HasOne(req => req.Sender).WithMany(user => user.Requests).HasForeignKey(req => req.SenderId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
