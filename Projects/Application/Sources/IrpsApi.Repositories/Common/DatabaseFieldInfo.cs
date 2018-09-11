namespace Noandishan.IrpsApi.Repositories.Common
{
    internal class DatabaseFieldInfo : IDatabaseFieldInfo
    {
        public string TableName
        {
            get;
            set;
        }

        public string FieldName
        {
            get;
            set;
        }

        public IFilterDataConvertor DataConvertor
        {
            get;
            set;
        }

        public string SqlCastDataType
        {
            get;
            set;
        }
    }
}
