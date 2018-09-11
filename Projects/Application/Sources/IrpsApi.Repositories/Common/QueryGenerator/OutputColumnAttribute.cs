using System;

namespace Noandishan.IrpsApi.Repositories.Common.QueryGenerator
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class OutputColumnAttribute : Attribute
    {
    }
}