using System;
using System.Collections.Generic;
using IrpsApi.Framework;
using Mabna.Data;
using Microsoft.Extensions.Configuration;

namespace Noandishan.IrpsApi.Repositories
{
    public abstract class EntityRepositoryBase<TEntity> : IEntityRepository<TEntity>, IRepository where TEntity : IEntity
    {
        private readonly IConfiguration _configuration;

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
    }
}
