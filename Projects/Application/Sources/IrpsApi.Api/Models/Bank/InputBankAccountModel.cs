﻿using System.Runtime.Serialization;
using IrpsApi.Api.Models.Accounts;
using IrpsApi.Api.ValidationHelpers;

namespace IrpsApi.Api.Models.Bank
{
    [DataContract]
    public class InputBankAccountModel
    {
        [DataMember(Name = "account")]
        [Required]
        public AccountModel Account
        {
            get;
            set;
        }

        [DataMember(Name = "bank_id")]
        public BankModel BankId
        {
            get;
            set;
        }

        [DataMember(Name = "number")]
        public decimal Number
        {
            get;
            set;
        }

        [DataMember(Name = "iban")]
        [Required]
        public string Iban
        {
            get;
            set;
        }

        [DataMember(Name = "branch_name")]
        public string BranchName
        {
            get;
            set;
        }

        [DataMember(Name = "branch_name_en")]
        public string BranchNameEn
        {
            get;
            set;
        }

        [DataMember(Name = "branch_code")]
        public string BranchCode
        {
            get;
            set;
        }

        [DataMember(Name = "type")]
        public BankAccountTypeModel Type
        {
            get;
            set;
        }

        [DataMember(Name = "daily_payment")]
        public bool DailyPayment
        {
            get;
            set;
        }
    }
}
