using System;
using System.Collections.Generic;
using System.Linq;

namespace IrpsApi.Framework
{
    public class EntityQuery<TEntity> where TEntity : IEntity
    {
        public EntityQueryFilter<TEntity> Filter
        {
            get;
            set;
        }

        public ICollection<EntityQuerySort<TEntity>> Sorts
        {
            get;
            set;
        }

        public EntityQueryLimit Limit
        {
            get;
            set;
        }

        public EntityQuery(EntityQueryFilter<TEntity> filter)
        {
            Filter = filter;
        }

        public EntityQuery(EntityQueryFilter<TEntity> filter, IEnumerable<EntityQuerySort<TEntity>> sorts)
        {
            Filter = filter;
            Sorts = sorts.ToArray();
        }

        public EntityQuery(EntityQueryFilter<TEntity> filter, IEnumerable<EntityQuerySort<TEntity>> sorts, EntityQueryLimit limit)
        {
            Filter = filter;
            Sorts = sorts.ToArray();
            Limit = limit;
        }
    }
}
