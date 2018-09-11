namespace Noandishan.IrpsApi.Repositories.Common.QueryGenerator
{
    public class ColumnInformation
    {
        private string _name;
        private string _propertyName;

        public string PropertyName
        {
            get
            {
                return _propertyName;
            }
            set
            {
                _propertyName = value;

                if (_propertyName.Contains("["))
                    ScapedPropertyName = value;
                else
                    ScapedPropertyName = $"[{value}]";
            }
        }

        public string ScapedPropertyName
        {
            get;
            private set;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;

                if (value.Contains("["))
                    ScapedName = value;
                else
                    ScapedName = $"[{value}]";
            }
        }

        public string ScapedName
        {
            get;
            private set;
        }

        public ColumnFlags Flags
        {
            get;
            set;
        }

        public string SqlType
        {
            get;
            set;
        }

        public ColumnInformation(string name)
        {
            Name = name;
        }
    }
}