namespace Noandishan.IrpsApi.Repositories.Common
{
    internal interface IFilterDataConvertor
    {
        object ConvertToDatabaseValue(object data);
    }
}
