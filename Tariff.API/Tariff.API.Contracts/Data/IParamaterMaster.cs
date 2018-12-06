namespace Tariff.API.Contracts.Data
{
    public interface IParameterMaster
    {
        //Model for ParameterMaster Table in Database
        //Primary Key
        int parameterId { get; set; }
        string parameterName { get; set; }
        byte isActive { get; set; }
    }
}
