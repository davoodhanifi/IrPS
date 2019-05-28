using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using IrpsApi.Framework.Accounts;
using IrpsApi.Framework.System;
using IrpsApi.Framework.System.Repositories;
using Mabna.Diagnostics;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.System
{
    public class LogRepository : GeneratedQueryEditableRecordRepository<ILog, Log>, ILogRepository
    {
        public LogRepository(IIrpsConnectionString connectionString) : base(connectionString)
        {
        }

        public void InsertLog(string source, string levelId, IAccount account, string action, params object[] parameterKeysAndValues)
        {
            InsertLogInternallyAsync(source, levelId, account, action, parameterKeysAndValues);
        }

        private async void InsertLogInternallyAsync(string source, string levelId, IAccount account, string action, params object[] parameterKeysAndValues)
        {
            try
            {
                var parameters = CreateParameter(parameterKeysAndValues).ToArray();
                if (parameters.Length == 0)
                    parameters = null;

                var accountLog = Create();
                accountLog.Source = source;
                accountLog.LevelId = levelId;
                accountLog.AccountId = account?.Id;
                accountLog.DateTime = DateTime.Now;
                accountLog.Action = action;
                accountLog.Parameters = parameters;

                await SaveAsync(accountLog, CancellationToken.None);
            }
            catch (Exception ex)
            {
                DefaultTraceSource.TraceWarning($"LogRepository: Error in log repositry.{Environment.NewLine}{ex}");
            }
        }

        private IEnumerable<ILogParameter> CreateParameter(object[] parameterKeysAndValues)
        {
            for (var i = 0; i < parameterKeysAndValues.Length; i++)
            {
                var key = parameterKeysAndValues[i].ToString();

                if (parameterKeysAndValues.Length > ++i && parameterKeysAndValues[i] != null)
                {
                    var parameter = new LogParameter();
                    parameter.Key = key;
                    parameter.Value = parameterKeysAndValues[i];

                    yield return parameter;
                }
            }
        }
    }
}