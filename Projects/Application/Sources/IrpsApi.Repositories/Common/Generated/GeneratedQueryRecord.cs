using System;
using IrpsApi.Framework;
using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;

namespace Noandishan.IrpsApi.Repositories.Common.Generated
{
    public class GeneratedQueryRecord : GeneratedQueryEntityBase, IRecord
    {
        [IgnoreColumn]
        public long RecordVersion
        {
            get;
            set;
        }

        [ColumnAlias("RecordVersion")]
        [IgnoreWrite]
        [OutputColumn]
        public byte[] InternalRecordVersion
        {
            get
            {
                return RecordVersion.ToRecordVersion();
            }
            set
            {
                RecordVersion = value.ToLongRecordVersion();
            }
        }

        [IgnoreWrite]
        [OutputColumn]
        public RecordState RecordState
        {
            get;
            set;
        }

        [IgnoreWrite]
        [OutputColumn]
        public DateTime? RecordInsertDateTime
        {
            get;
            set;
        }

        [IgnoreWrite]
        [OutputColumn]
        public DateTime? RecordUpdateDateTime
        {
            get;
            set;
        }

        [IgnoreWrite]
        [OutputColumn]
        public DateTime? RecordDeleteDateTime
        {
            get;
            set;
        }
    }
}