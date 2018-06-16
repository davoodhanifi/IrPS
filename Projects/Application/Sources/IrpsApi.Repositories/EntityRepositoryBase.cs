using System;
using System.Collections.Generic;
using System.Data;
using IrpsApi.Framework;
using Mabna.Data;
using Microsoft.Extensions.Configuration;

namespace Noandishan.IrpsApi.Repositories
{
    public abstract class EntityRepositoryBase<TEntity> : IEntityRepository<TEntity>, IRepository where TEntity : IEntity
    {
        private readonly IConfiguration _configuration;
        private readonly IDictionary<int, TEntity> _entityCache = new Dictionary<int, TEntity>();

        public EntityRepositoryBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Load(IEntity entity)
        {
            throw new NotImplementedException();
        }

        IEntity IRepository.Create()
        {
            return Create();
        }

        IEntity IEntityRepository.Create()
        {
            return Create();
        }

        public void Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Upsert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll(EntityQuery<TEntity> query)
        {
            throw new NotImplementedException();
        }

        public int GetCount(EntityQuery<TEntity> query)
        {
            throw new NotImplementedException();
        }

        public TEntity Create()
        {
            throw new NotImplementedException();
        }

        public void Insert(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public Type GetEntityType()
        {
            throw new NotImplementedException();
        }

        protected DataCommand GetCommand()
        {
            return new DataCommand(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected virtual void SetEntityCore(TEntity entity, IDataReader reader)
        {
            entity.Id = reader.ReadInt32("Id");
        }

        protected int ExecuteNonQuery(DataCommand command)
        {
            var rowsAffected = command.ExecuteNonQuery();

            return rowsAffected;
        }

        protected T ExecuteScalar<T>(DataCommand command)
        {
            var reader = (T)command.ExecuteScalar();

            return reader;
        }

        protected IDataReader ExecuteReader(DataCommand command)
        {
            var reader = command.ExecuteReader();

            return reader;
        }


        protected TEntity FetchEntity(DataCommand command)
        {
            using (var reader = ExecuteReader(command))
            {
                var entity = ReadSingleEntity(reader);

                if (entity == null)
                    return default(TEntity);

                return CacheEntity(entity);
            }
        }

        protected ICollection<TEntity> FetchEntities(DataCommand command)
        {
            var entities = new List<TEntity>();

            using (var reader = ExecuteReader(command))
                foreach (var entity in ReadMultipleEntities(reader))
                    entities.Add(CacheEntity(entity));

            return entities;
        }

        private TEntity CacheEntity(TEntity entity)
        {
            TEntity cachedEntity;

            // If this entity already exists in cache, return the cached one for consistency
            if (_entityCache.TryGetValue(entity.Id, out cachedEntity))
                return cachedEntity;

            _entityCache[entity.Id] = entity;

            return entity;
        }

        private TEntity ReadSingleEntity(IDataReader reader)
        {
            if (!reader.Read())
                return default(TEntity);

            return SetEntity(reader);
        }

        private IEnumerable<TEntity> ReadMultipleEntities(IDataReader reader)
        {
            while (reader.Read())
                yield return SetEntity(reader);
        }

        protected abstract TEntity SetEntity(IDataReader reader);
    }
}
