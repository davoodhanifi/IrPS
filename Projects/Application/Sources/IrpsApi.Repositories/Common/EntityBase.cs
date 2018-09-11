using System.Data;
using IrpsApi.Framework;

namespace Noandishan.IrpsApi.Repositories.Common
{
    public class EntityBase : IEntity
    {
      
        public virtual string Id
        {
            get;
            set;
        }

        internal virtual void Load(IDataRecord record)
        {
            Id = record["Id"].ToString();
        }

        protected static IEntity GetEntity(string id)
        {
            var entity = new EntityBase();
            entity.Id = id;

            return entity;
        }
    }
}
