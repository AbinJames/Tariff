using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tariff.Data.Models
{
    public class InvoiceMaster
    {
        //Model for main InvoiceMaster Table in Database

        [Key]
        //Primary key
        public int invoiceId { get; set; }

        [Required(ErrorMessage = "Enter name")]
        //regular expression for match only alphabets
        public string invoiceName { get; set; }

        [Required]
        public byte isActive { get; set; }

        public ICollection<RuleDetails> ruleDetails { get; set; }
    }
}
