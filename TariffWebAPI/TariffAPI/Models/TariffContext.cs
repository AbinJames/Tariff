using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TariffAPI.Models.TariffConfig;

namespace TariffAPI.Models
{
    public class TariffContext:DbContext
    {
        public TariffContext(DbContextOptions<TariffContext> options) : base(options)
        {

        }

        //Applying StudentConfigurations to Model Tables
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Apply cofiguration specified to corresponding Models
            modelBuilder.ApplyConfiguration<InvoiceMaster>(new TariffConfiguration());
            modelBuilder.ApplyConfiguration<ParameterMaster>(new TariffConfiguration());
            modelBuilder.ApplyConfiguration<RuleDetails>(new TariffConfiguration());
        }

        //Set DbSet for following classes to access from database
        public DbSet<InvoiceMaster> InvoiceMaster { get; set; }
        public DbSet<ParameterMaster> ParameterMaster { get; set; }
        public DbSet<RuleDetails> RuleDetails { get; set; }
    }
}
