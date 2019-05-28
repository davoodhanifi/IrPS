using System;
using System.Collections.Generic;
using System.Data;
using IrpsApi.Framework.System;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;

namespace Noandishan.IrpsApi.Repositories.System
{
    [Table("Log", "System")]
    public class Log : GeneratedQueryRecord, ILog
    {
        private IEnumerable<ILogParameter> _parameters;

        public string Source
        {
            get;
            set;
        }

        public string LevelId
        {
            get;
            set;
        }

        public string AccountId
        {
            get;
            set;
        }

        public DateTime DateTime
        {
            get;
            set;
        }

        public string Action
        {
            get;
            set;
        }

        public string Parameters
        {
            get => _parameters.SerializeParameters();
            set => _parameters = SerializeHelper.DesrializeParameters(value);
        }

        [IgnoreColumn]
        IEnumerable<ILogParameter> ILog.Parameters
        {
            get => _parameters;
            set => _parameters = value;
        }
    }
}
