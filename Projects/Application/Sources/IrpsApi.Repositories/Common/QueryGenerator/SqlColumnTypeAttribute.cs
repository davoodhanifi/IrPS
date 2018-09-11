using System;

namespace Noandishan.IrpsApi.Repositories.Common.QueryGenerator
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class SqlColumnTypeAttribute : Attribute
    {
        public string FullType { get; set; }

        public SqlColumnTypeAttribute(string fullType)
        {
            FullType = fullType;
        }
    }
}