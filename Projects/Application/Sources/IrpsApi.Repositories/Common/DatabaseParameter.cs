namespace Noandishan.IrpsApi.Repositories.Common
{
    public class DatabaseParameter
    {
        public string Name
        {
            get;
            set;
        }

        public object Value
        {
            get;
            set;
        }

        internal DatabaseParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
