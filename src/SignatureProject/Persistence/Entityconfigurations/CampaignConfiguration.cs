using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Entityconfigurations;

public class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
{
    public void Configure(EntityTypeBuilder<Campaign> builder)
    {
        builder.ToTable("Campaigns").HasKey(ea => ea.Id);

        builder.Property(ea => ea.Id).HasColumnName("Id").IsRequired();

        builder.Property(ea => ea.Name).HasColumnName("Name").IsRequired();
        builder.Property(ea => ea.Description).HasColumnName("Description").IsRequired();
        builder.Property(ea => ea.ExpiredDate).HasColumnName("ExpiredDate").IsRequired();
        builder.Property(ea => ea.Quantity).HasColumnName("Quantity").IsRequired();

        builder.Property(ea => ea.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ea => ea.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ea => ea.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ea => !ea.DeletedDate.HasValue);
    }
}
