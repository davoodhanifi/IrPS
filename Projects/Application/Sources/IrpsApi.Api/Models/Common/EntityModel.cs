using System.Runtime.Serialization;

namespace IrpsApi.Api.Models.Common
{
    [DataContract(Name = "entiy")]
    public class EntityModel
    {
        [DataMember(Name = "id")]
        public string Id
        {
            get;
            set;
        }
    }
}
