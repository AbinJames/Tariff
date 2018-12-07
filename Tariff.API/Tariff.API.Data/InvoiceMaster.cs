using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tariff.API.Contracts.Data;

namespace Tariff.API.Data
{
    public class InvoiceMaster:IInvoiceMaster
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

        public IEnumerable<RuleDetails> ruleView { get; set; }
    }
}
