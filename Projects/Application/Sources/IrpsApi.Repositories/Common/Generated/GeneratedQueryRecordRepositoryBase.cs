using System.Collections.Generic;
using System.Linq;
using IrpsApi.Framework;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.Common.Generated
{
    public class GeneratedQueryRecordRepositoryBase<TRecord, TDb> : GeneratedQueryQueryableEntityRepositoryBase<TRecord, TDb>
        where TDb : GeneratedQueryRecord, new()
        where TRecord : IRecord
    {
        public GeneratedQueryRecordRepositoryBase(IConnectionString connectionString) : base(connectionString)
        {
            Initialize();
        }

        private void Initialize()
        {
            AddFieldInfo("RecordVersion", new RecordVersionDataConvertor());
            AddFieldInfo("RecordState", new EnumTypeTypeConvertor<RecordState>());
        }

        public override List<IFilterParameter> GetExtraFilters(IEnumerable<IFilterParameter> filterParameters)
        {
            var filters = base.GetExtraFilters(filterParameters);

            if ((filterParameters == null || !filterParameters.Any(f => f.FieldName == "RecordState" || f.FieldName == "RecordVersion")) && (filters == null || !filters.Any(f => f.FieldName == "RecordState" || f.FieldName == "RecordVersion")))
                filters.Add(new FilterParameter("RecordState", FilterOperatorType.NotEqual, RecordState.Deleted));

            return filters;
        }
    }
}