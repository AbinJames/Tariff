using Microsoft.EntityFrameworkCore;
using Tariff.API.Data;

namespace Tariff.Data.Service.Context
{
    public static class TariffSeeder
    {
        //This function is used to populate the db with initial data
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceMaster>().HasData(
                new InvoiceMaster { invoiceId = 1, invoiceName = "Sample Invoice 1", isActive = 1 },
                new InvoiceMaster { invoiceId = 2, invoiceName = "Sample invoice 2", isActive = 1 }
                );
            modelBuilder.Entity<ParameterMaster>().HasData(
                new ParameterMaster { parameterId = 1, parameterName = "Vessel Capacity", isActive = 1 },
                new ParameterMaster { parameterId = 2, parameterName = "Vessel Type", isActive = 1 },
                new ParameterMaster { parameterId = 3, parameterName = "LOA", isActive = 1 }
                );
            modelBuilder.Entity<RuleDetails>().HasData(
                new RuleDetails { ruleId = 1, invoiceId = 1, parameterId = 1, ruleValue = "Sample Rule 1", isActive = 1 },
                new RuleDetails { ruleId = 2, invoiceId = 1, parameterId = 2, ruleValue = "Sample Rule 2", isActive = 1 },
                new RuleDetails { ruleId = 3, invoiceId = 1, parameterId = 3, ruleValue = "Sample Rule 3", isActive = 1 },
                new RuleDetails { ruleId = 4, invoiceId = 2, parameterId = 1, ruleValue = "Sample Rule 4", isActive = 1 },
                new RuleDetails { ruleId = 5, invoiceId = 2, parameterId = 2, ruleValue = "Sample Rule 5", isActive = 1 },
                new RuleDetails { ruleId = 6, invoiceId = 2, parameterId = 3, ruleValue = "Sample Rule 6", isActive = 1 }
                );
        }
    }
}
