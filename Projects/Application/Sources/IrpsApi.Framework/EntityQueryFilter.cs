using System;
using System.Linq.Expressions;

namespace IrpsApi.Framework
{
    public class EntityQueryFilter<TEntity> where TEntity : IEntity
    {
        public Expression<Func<TEntity, bool>> Predicate
        {
            get;
            set;
        }

        public EntityQueryFilter(Expression<Func<TEntity, bool>> predicate)
        {
            Predicate = predicate;
        }
    }
}
