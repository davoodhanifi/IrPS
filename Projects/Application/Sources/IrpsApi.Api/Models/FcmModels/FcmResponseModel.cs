using System.Runtime.Serialization;

namespace IrpsApi.Api.Models.FcmModels
{
    [DataContract]
    public class FcmResponseModel
    {
        [DataMember(Name = "multicast_id")]
        public long MulticastId
        {
            get;
            set;
        }

        [DataMember(Name = "success")]
        public int Success
        {
            get;
            set;
        }

        [DataMember(Name = "failure")]
        public int Failure
        {
            get;
            set;
        }

        [DataMember(Name = "canonical_ids")]
        public int CanonicalIds
        {
            get;
            set;
        }

        [DataMember(Name = "results")]
        public Result[] Results
        {
            get;
            set;
        }

        [DataContract]
        public class Result
        {
            [DataMember(Name = "message_id")]
            public string MessageId
            {
                get;
                set;
            }
        }
    }
}