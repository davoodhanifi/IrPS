using System;
using System.Linq.Expressions;

namespace IrpsApi.Framework
{
    public class EntityQuerySort<TEntity> where TEntity : IEntity
    {
        public Expression<Func<TEntity, object>> KeySelector
        {
            get;
            set;
        }

        public SortDirection Direction
        {
            get;
            set;
        }

        public EntityQuerySort(Expression<Func<TEntity, object>> keySelector)
        {
            KeySelector = keySelector;
        }

        public EntityQuerySort(Expression<Func<TEntity, object>> keySelector, SortDirection direction)
        {
            KeySelector = keySelector;
            Direction = direction;
        }
    }
}
