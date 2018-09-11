using System;

namespace Noandishan.IrpsApi.Repositories.Common.QueryGenerator
{
    [Flags]
    public enum ColumnFlags
    {
        PrimaryKey = 1,
        Identiy = 2,
        IgnoreWrite = 4,
        OutputColumn = 8
    }
}