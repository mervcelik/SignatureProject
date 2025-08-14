using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Entityconfigurations;

public class CampaignUserConfiguration : IEntityTypeConfiguration<CampaignUser>
{
    public void Configure(EntityTypeBuilder<CampaignUser> builder)
    {
        builder.ToTable("CampaignUsers").HasKey(ea => ea.Id);

        builder.Property(ea => ea.Id).HasColumnName("Id").IsRequired();
        builder.Property(ea => ea.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(ea => ea.CampaignId).HasColumnName("CampaignId").IsRequired();
        builder.Property(ea => ea.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ea => ea.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ea => ea.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ea => !ea.DeletedDate.HasValue);

        builder.HasOne(ea => ea.User);
        builder.HasOne(ea => ea.Campaign);
    }
}
