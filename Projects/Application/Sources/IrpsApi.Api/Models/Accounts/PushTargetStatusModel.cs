﻿using System;
using System.Runtime.Serialization;
using IrpsApi.Api.Models.Common;

namespace IrpsApi.Api.Models.Accounts
{
    [DataContract(Name = "push_target_status")]
    public class PushTargetStatusModel : RecordModel
    {
        [DataMember(Name = "title")]
        public string Title
        {
            get;
            set;
        }

        [DataMember(Name = "title_en")]
        public string TitleEn
        {
            get;
            set;
        }
    }
}