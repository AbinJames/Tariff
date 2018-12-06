using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tariff.Data.Service.Models
{
    //Model for recieving data from clientside
    public class Rule
    {
        //Model for getting list of rules of corresponding invoice
        public string parameterId { get; set; }
        public string ruleValue { get; set; }
        public bool isActive { get; set; }
    }
    public class InvoicePostModel
    {
        //Model for invoice entered by client
        [Key]
        //Primary Key
        public int id { get; set; }

        public string invoiceName { get; set; }
        //List of rules entered by client
        public IEnumerable<Rule> ruleList { get; set; }
        public bool isActive { get; set; }
    }
}
