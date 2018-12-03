using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TariffAPI.Models.TariffConfig
{
    public class TariffConfiguration : IEntityTypeConfiguration<InvoiceMaster>,
        IEntityTypeConfiguration<ParameterMaster>,
        IEntityTypeConfiguration<RuleDetails>
    {
        //Configure InvoiceMaster for Database
        public void Configure(EntityTypeBuilder<InvoiceMaster> builder)
        {
            builder.HasKey(prop => prop.invoiceId);
            builder.Property(prop => prop.invoiceName)
                .HasMaxLength(50)
                .IsRequired();
        }

        //Configure ParameterMaster for Database
        public void Configure(EntityTypeBuilder<ParameterMaster> builder)
        {
            builder.HasKey(prop => prop.parameterId);
            builder.Property(prop => prop.parameterName)
                .HasMaxLength(50)
                .IsRequired();
        }

        //Configure RuleDetails for Database
        public void Configure(EntityTypeBuilder<RuleDetails> builder)
        {
            builder.HasKey(prop => prop.ruleId);
            builder.Property(prop => prop.ruleValue)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
