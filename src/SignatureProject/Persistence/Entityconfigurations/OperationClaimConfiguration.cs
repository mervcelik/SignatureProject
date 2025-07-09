using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Entityconfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

        builder.HasMany(oc => oc.UserOperationClaims);

        //builder.HasData(_seeds);
        builder.Property(u => u.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(u => u.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(u => u.DeletedDate).HasColumnName("DeletedDate");


        builder.HasQueryFilter(u => !u.DeletedDate.HasValue);
    }

    //private IEnumerable<OperationClaim> _seeds
    //{
    //    get
    //    {
    //        int id = 0;

    //        yield return new OperationClaim { Id = ++id, Name = GeneralOperationClaims.Admin };

    //        #region Feature Operation Claims
    //        IEnumerable<Type> featureOperationClaimsTypes = Assembly
    //            .GetAssembly(typeof(ApplicationServiceRegistration))!
    //            .GetTypes()
    //            .Where(
    //                type =>
    //                    (type.Namespace?.Contains("Features") == true)
    //                    && (type.Namespace?.Contains("Constants") == true)
    //                    && type.IsClass
    //                    && type.Name.EndsWith("OperationClaims")
    //            );
    //        foreach (Type type in featureOperationClaimsTypes)
    //        {
    //            FieldInfo[] typeFields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
    //            IEnumerable<string> typeFieldsValues = typeFields.Select(field => field.GetValue(null)!.ToString()!);

    //            IEnumerable<OperationClaim> featureOperationClaimsToAdd = typeFieldsValues.Select(
    //                value => new OperationClaim { Id = ++id, Name = value }
    //            );
    //            foreach (OperationClaim featureOperationClaim in featureOperationClaimsToAdd)
    //                yield return featureOperationClaim;
    //        }
    //        #endregion
    //    }
    //}
}
