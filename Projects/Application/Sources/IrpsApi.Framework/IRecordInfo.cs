using System;

namespace IrpsApi.Framework
{
    public interface IRecordInfo
    {
        long Version
        {
            get;
        }

        RecordState State
        {
            get;
        }

        DateTime? InsertDateTime
        {
            get;
        }

        DateTime? UpdateDateTime
        {
            get;
        }

        DateTime? DeleteDateTime
        {
            get;
        }
    }
}
