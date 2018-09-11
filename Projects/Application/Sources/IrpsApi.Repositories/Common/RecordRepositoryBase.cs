using System;
using System.Collections.Generic;
using System.Linq;
using IrpsApi.Framework;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.Common
{
    public abstract class RecordRepositoryBase<TRecord> : QueryableEntityRepositoryBase<TRecord> where TRecord : IRecord
    {
        protected RecordRepositoryBase(string baseTableName, IConnectionString connectionString) : base(baseTableName, connectionString)
        {
            Initialize(baseTableName);
        }

        private void Initialize(string baseTableName)
        {
            AddFieldInfo("RecordVersion", new RecordVersionDataConvertor());
            AddFieldInfo("RecordState", new EnumTypeTypeConvertor<RecordState>());
        }

        public override List<IFilterParameter> GetExtraFilters(IEnumerable<IFilterParameter> filterParameters)
        {
            var filters = base.GetExtraFilters(filterParameters);

            if (filterParameters == null || !filterParameters.Any(f => string.Equals(f.FieldName, "RecordState", StringComparison.OrdinalIgnoreCase) ||  string.Equals(f.FieldName, "RecordVersion", StringComparison.OrdinalIgnoreCase)))
                filters.Add(new FilterParameter("RecordState", FilterOperatorType.NotEqual, RecordState.Deleted));

            return filters;
        }
    }
}
