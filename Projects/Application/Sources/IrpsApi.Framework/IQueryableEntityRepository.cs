using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework
{
    public interface IQueryableEntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : IEntity
    {
        Task<IEnumerable<TEntity>> GetAsync(IEnumerable<IFilterParameter> filterParameters = null, IEnumerable<ISortOption> sortOptions = null, IPagingOption pagingOption = null, CancellationToken cancellationToken = default, Operator @operator = Operator.And);

        Task<IEnumerable<TEntity>> GetAsync(IQuery query = null, CancellationToken cancellationToken = default);

        Task<int> GetCountAsync(IEnumerable<IFilterParameter> filterParameters, CancellationToken cancellationToken = default, Operator @operator = Operator.And);

        Task<int> GetCountAsync(IQuery query = null, CancellationToken cancellationToken = default);
    }
}
