using IrpsApi.Framework;

namespace Noandishan.IrpsApi.Repositories.Common
{
    internal class FilterParameter : IFilterParameter
    {
        public string FieldName
        {
            get;
            set;
        }

        public FilterOperatorType Operator
        {
            get;
            set;
        }

        public object[] Values
        {
            get;
            set;
        }

        public FilterParameter()
        {
        }

        public FilterParameter(string name, FilterOperatorType operatorType, object[] values)
        {
            FieldName = name;
            Operator = operatorType;
            Values = values;
        }

        public FilterParameter(string name, FilterOperatorType operatorType, object value) : this(name, operatorType, new[] { value })
        {
        }

        public FilterParameter(string name, object value) : this(name, FilterOperatorType.Equal, new[] { value })
        {
        }
    }
}
