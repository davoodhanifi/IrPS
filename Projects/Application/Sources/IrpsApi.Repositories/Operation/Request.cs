using System;
using System.Collections.Generic;
using IrpsApi.Framework.Operation;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;

namespace Noandishan.IrpsApi.Repositories.Operation
{
    [Table("Request", "Operation")]
    public class Request : GeneratedQueryRecord, IRequest
    {
        private IEnumerable<IRequestParameter> _parameters;

        public string AccountId
        {
            get;
            set;
        }

        public string TypeId
        {
            get;
            set;
        }

        public DateTime DateTime
        {
            get;
            set;
        }

        public string StatusId
        {
            get;
            set;
        }

        public string Comments
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
        IEnumerable<IRequestParameter> IRequest.Parameters
        {
            get => _parameters;
            set => _parameters = value;
        }
    }
}
