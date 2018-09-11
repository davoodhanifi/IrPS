using System;

namespace Noandishan.IrpsApi.Repositories.Common
{
    internal class RecordVersionDataConvertor : IFilterDataConvertor
    {
        public object ConvertToDatabaseValue(object data)
        {
            if (data is long)
                return ((long)data).ToRecordVersion();

            throw new ArgumentException("RecordVersion must be long integer number");
        }
    }
}
