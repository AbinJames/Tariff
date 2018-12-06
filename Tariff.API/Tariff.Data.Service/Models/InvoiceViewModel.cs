using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tariff.Data.Service.Models
{
    public class InvoiceViewModel
    {
        //Model for sending data as JSON to clientside
        //Data computed through LINQ is saved as list of InvoiceViewModel objects

        [Key]
        //Primary key
        public int id { get; set; }

        public string invoiceName { get; set; }

        //List of rules for corresponding invoive from InvoiceMaster
        public IEnumerable<RuleViewModel> ruleView { get; set; }

        public byte isActive { get; set; }
    }
}
