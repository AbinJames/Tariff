using System.ComponentModel.DataAnnotations;
using Tariff.API.Contracts.Data;

namespace Tariff.API.Data
{
    public class RuleDetails:IRuleDetails
    {
        //Model for RuleDetails Table in Database

        [Key]
        //Primary Key
        public int ruleId { get; set; }

        //Foreignkey connection with InvoiceMaster
        public int invoiceId { get; set; }
        public InvoiceMaster invoiceMaster { get; set; }

        //Foreignkey Connection with ParameterMaster
        public int parameterId { get; set; }
        public ParameterMaster parameterMaster { get; set; }

        [Required(ErrorMessage = "Enter value")]
        public string ruleValue { get; set; }

        [Required]
        public byte isActive { get; set; }
    }
}
