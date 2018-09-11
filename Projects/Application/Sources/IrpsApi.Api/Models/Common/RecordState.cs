using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IrpsApi.Api.Models.Common
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RecordState
    {
        [EnumMember(Value = "inserted")]
        Inserted = 0,

        [EnumMember(Value = "updated")]
        Updated = 1,

        [EnumMember(Value = "deleted")]
        Deleted = 2
    }
}
