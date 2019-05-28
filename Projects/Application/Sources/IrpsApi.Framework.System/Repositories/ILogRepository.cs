using IrpsApi.Framework.Accounts;

namespace IrpsApi.Framework.System.Repositories
{
    public interface ILogRepository : IEditableEntityRepository<ILog>
    {
        void InsertLog(string source, string levelId, IAccount account, string action, params object[] parameterKeysAndValues);
    }
}
