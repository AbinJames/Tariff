namespace Tariff.API.Contracts.Data
{
    public interface IInvoiceMaster
    {
        //Model for main InvoiceMaster Table in Database
        //Primary key
        int invoiceId { get; set; }
        string invoiceName { get; set; }
        byte isActive { get; set; }
    }
}
