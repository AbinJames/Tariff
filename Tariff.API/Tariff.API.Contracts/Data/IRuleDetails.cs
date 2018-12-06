namespace Tariff.API.Contracts.Data
{
    public interface IRuleDetails
    {
        //Model for RuleDetails Table in Database
        //Primary Key
        int ruleId { get; set; }

        //Foreignkey connection with InvoiceMaster
        int invoiceId { get; set; }

        //Foreignkey Connection with ParameterMaster
        int parameterId { get; set; }

        string ruleValue { get; set; }
        byte isActive { get; set; }
    }
}
