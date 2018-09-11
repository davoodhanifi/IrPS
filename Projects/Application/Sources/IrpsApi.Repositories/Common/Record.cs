using System;
using System.Data;
using IrpsApi.Framework;

namespace Noandishan.IrpsApi.Repositories.Common
{
    internal class Record : EntityBase, IRecord
    {
        public long RecordVersion
        {
            get;
            set;
        }

        public RecordState RecordState
        {
            get;
            set;
        }

        public DateTime? RecordInsertDateTime
        {
            get;
            set;
        }

        public DateTime? RecordUpdateDateTime
        {
            get;
            set;
        }

        public DateTime? RecordDeleteDateTime
        {
            get;
            set;
        }

        internal override void Load(IDataRecord record)
        {
            base.Load(record);
            RecordVersion = record.GetRecordVersion("RecordVersion");
            RecordState = (RecordState)(int)record["RecordState"];
            RecordInsertDateTime = record["RecordInsertDateTime"] as DateTime?;
            RecordUpdateDateTime = record["RecordUpdateDateTime"] as DateTime?;
            RecordDeleteDateTime = record["RecordDeleteDateTime"] as DateTime?;
        }
    }
}
