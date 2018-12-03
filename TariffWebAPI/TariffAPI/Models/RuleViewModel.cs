using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TariffAPI.Models
{
    public class RuleViewModel
    {
        //Model for sending data as JSON to clientside
        //Data computed through LINQ as list of RuleViewModel objects
        //List is saved in InvoiceViewModel for corresponding invoice

        [Key]
        //Primary Key
        public int id { get; set; }
        public int invoiceId { get; set; }
        public string parameterName { get; set; }
        public string ruleValue { get; set; }
        public byte isActive { get; set; }
    }
}
