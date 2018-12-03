using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TariffAPI.Models
{
    public class RuleDetails
    {
        //Model for RuleDetails Table in Database

        [Key]
        //Primary Key
        public int ruleId { get; set; }

        [ForeignKey("InvoiceMaster")]
        //Foreignkey connection with InvoiceMaster
        public int invoiceId { get; set; }

        [ForeignKey("ParameterMaster")]
        //Foreignkey Connection with ParameterMaster
        public int parameterId { get; set; }

        [Required(ErrorMessage = "Enter value")]
        public string ruleValue { get; set; }

        [Required]
        public byte isActive { get; set; }
    }
}
