using System;

namespace Noandishan.IrpsApi.Repositories.Common.QueryGenerator
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ColumnAliasAttribute : Attribute
    {
        public string Name { get; set; }

        public ColumnAliasAttribute(string name)
        {
            Name = name;
        }
    }
}