using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tariff.API.Data;

namespace Tariff.Data.Service.Context
{
    public class TariffContext : DbContext
    {
        public TariffContext(DbContextOptions<TariffContext> options) : base(options)
        {

        }

        //Set DbSet for following classes to access from database
        public DbSet<InvoiceMaster> InvoiceMaster { get; set; }
        public DbSet<ParameterMaster> ParameterMaster { get; set; }
        public DbSet<RuleDetails> RuleDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("Tariff"); // Specify the default schema for this application
            modelBuilder.SeedData(); // An extension method written specifically for adding the seed data
        }
    }
}
