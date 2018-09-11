using System;

namespace Noandishan.IrpsApi.Repositories.Common.QueryGenerator
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        public string Name
        {
            get;
            set;
        }

        public string Schema
        {
            get;
            set;
        }

        public TableAttribute(string name = null, string schema = null)
        {
            Name = name;
            Schema = schema;
        }
    }
}