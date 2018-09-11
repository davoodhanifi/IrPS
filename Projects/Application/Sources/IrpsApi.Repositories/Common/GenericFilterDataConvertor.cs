namespace Noandishan.IrpsApi.Repositories.Common
{
    internal class GenericFilterDataConvertor : IFilterDataConvertor
    {
        public object ConvertToDatabaseValue(object data)
        {
            return data;
        }
    }
}
