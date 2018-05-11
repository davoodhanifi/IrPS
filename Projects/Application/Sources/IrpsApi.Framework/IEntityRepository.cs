using System;
using System.Collections.Generic;

namespace IrpsApi.Framework
{
    public interface IEntityRepository<TEntity> : IEntityRepository where TEntity : IEntity
    {
        new TEntity Create();

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Upsert(TEntity entity);

        void Delete(TEntity entity);

        TEntity Get(int id);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAll(IEnumerable<int> ids);

        IEnumerable<TEntity> GetAll(EntityQuery<TEntity> query);

        int GetCount(EntityQuery<TEntity> query);
    }

    public interface IEntityRepository
    {
        IEntity Create();

        void Insert(IEntity entity);

        Type GetEntityType();
    }
}
