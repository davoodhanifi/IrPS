namespace IrpsApi.Framework
{
    public interface IFilterParameter
    {
        string FieldName
        {
            get;
        }

        FilterOperatorType Operator
        {
            get;
        }

        object[] Values
        {
            get;
        }
    }
}
