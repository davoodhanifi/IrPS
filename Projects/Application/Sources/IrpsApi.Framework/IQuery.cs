using System.Collections.Generic;

namespace IrpsApi.Framework
{
    public interface IQuery
    {
        IEnumerable<IFilterParameter> FilterParameters
        {
            get;
        }

        IEnumerable<ISortOption> SortOptions
        {
            get;
        }

        IPagingOption PagingOption
        {
            get;
        }

        Operator Operator
        {
            get;
        }
    }
}