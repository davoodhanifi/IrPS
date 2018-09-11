namespace Noandishan.IrpsApi.Repositories.Common
{
    internal interface IDatabaseFieldInfo
    {

        string TableName
        {
            get;
        }

        string FieldName
        {
            get;
        }

        IFilterDataConvertor DataConvertor
        {
            get;
        }

        string SqlCastDataType
        {
            get;
        }
    }
}
