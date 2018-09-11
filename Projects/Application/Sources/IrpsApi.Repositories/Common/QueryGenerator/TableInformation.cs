using System;
using System.Collections.Generic;
using System.Linq;

namespace Noandishan.IrpsApi.Repositories.Common.QueryGenerator
{
    public class TableInformation
    {
        private string _schema;
        private string _name;
        private string _identifier;

        public string Schema
        {
            get
            {
                return _schema;
            }
            set
            {
                _schema = GetScapedValue(value);
                GenerateIdentifier();
            }
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
                ScapedName = GetScapedValue(value);
                GenerateIdentifier();
            }
        }

        public string ScapedName
        {
            get;
            private set;
        }

        public string Identifier => _identifier;

        private void GenerateIdentifier()
        {
            if (!string.IsNullOrWhiteSpace(_schema))
                _identifier = $"{Schema}.{ScapedName}";
            else
                _identifier = Name;
        }

        private string GetScapedValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            if (value.Contains("["))
                return value;
            else
                return $"[{value}]";
        }

        public ColumnInformation[] Columns
        {
            get;
            set;
        }

        public Type Type
        {
            get;
            set;
        }

        public TableInformation(string name)
        {
            Name = name;
        }

        public ColumnInformation GetPrimaryKeyColumn()
        {
            var attribSpecifiedCol = Columns.FirstOrDefault(q => q.Flags.HasFlag(ColumnFlags.PrimaryKey));
            if (attribSpecifiedCol != null)
                return attribSpecifiedCol;

            return Columns.FirstOrDefault(q => q.Name.Equals("id", StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<ColumnInformation> WritableColumns => Columns.Where(q => !q.Flags.HasFlag(ColumnFlags.IgnoreWrite));
    }
}