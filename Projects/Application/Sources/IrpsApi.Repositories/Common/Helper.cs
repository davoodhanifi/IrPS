using System;
using System.Data;
using System.Linq;
using IrpsApi.Framework;

namespace Noandishan.IrpsApi.Repositories.Common
{
    internal static class Helper
    {
        public static IRecordInfo GetRecordInfo(this IDataRecord record)
        {
            return new RecordInfo
            {
                Version = record.GetRecordVersion("RecordVersion"),
                State = (RecordState)(int)record["RecordState"],
                InsertDateTime = record["RecordInsertDateTime"] as DateTime?,
                UpdateDateTime = record["RecordUpdateDateTime"] as DateTime?,
                DeleteDateTime = record["RecordDeleteDateTime"] as DateTime?
            };
        }

        public static long GetRecordVersion(this IDataRecord record, string name)
        {
            var bytes = (byte[])record[name];

            if (BitConverter.IsLittleEndian)
                bytes = bytes.Reverse().ToArray();

            return BitConverter.ToInt64(bytes, 0);
        }

        public static long ToLongRecordVersion(this byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
                bytes = bytes.Reverse().ToArray();

            return BitConverter.ToInt64(bytes, 0);
        }

        public static byte[] ToRecordVersion(this long recordVersion)
        {
            if (BitConverter.IsLittleEndian)
                return BitConverter.GetBytes(recordVersion).Reverse().ToArray();

            return BitConverter.GetBytes(recordVersion);
        }
    }
}
