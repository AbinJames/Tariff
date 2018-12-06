using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tariff.Data.Models;

namespace Tariff.Data.Services.Context
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
    }
}
