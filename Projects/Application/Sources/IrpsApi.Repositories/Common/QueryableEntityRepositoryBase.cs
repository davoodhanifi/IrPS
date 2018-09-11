using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Framework;
using Mabna.Data;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.Common
{
    public abstract class QueryableEntityRepositoryBase<TEntity> : EntityRepositoryBase<TEntity>, IQueryableEntityRepository<TEntity> where TEntity : IEntity
    {
        private ConcurrentDictionary<string, IDatabaseFieldInfo> _complexFields = new ConcurrentDictionary<string, IDatabaseFieldInfo>(StringComparer.OrdinalIgnoreCase);

        private readonly string _baseTableName;

        protected abstract string GetAllQuery
        {
            get;
        }

        protected abstract string GetCountQuery
        {
            get;
        }

        protected QueryableEntityRepositoryBase(string baseTableName, IConnectionString connectionString) : base(connectionString)
        {
            _baseTableName = baseTableName;
        }

        internal void AddFieldInfo(string fieldName, IDatabaseFieldInfo fieldInfo)
        {
            _complexFields.AddOrUpdate(fieldName, fieldInfo, (key, old) => fieldInfo);
        }

        internal void AddFieldInfo(string fieldName, string tableName)
        {
            AddFieldInfo(fieldName, new DatabaseFieldInfo
            {
                FieldName = fieldName,
                TableName = tableName,
                DataConvertor = new GenericFilterDataConvertor()
            });
        }

        internal void AddFieldInfo(string fieldName, string tableName, string databaseFieldName)
        {
            AddFieldInfo(fieldName, new DatabaseFieldInfo
            {
                FieldName = databaseFieldName,
                TableName = tableName,
                DataConvertor = new GenericFilterDataConvertor()
            });
        }

        internal void AddFieldInfo(string fieldName, string tableName, string databaseFieldName, string sqlCastDataType)
        {
            AddFieldInfo(fieldName, new DatabaseFieldInfo
            {
                FieldName = databaseFieldName,
                TableName = tableName,
                SqlCastDataType = sqlCastDataType,
                DataConvertor = new GenericFilterDataConvertor()
            });
        }

        internal void AddFieldInfo(string fieldName, IFilterDataConvertor convertor)
        {
            AddFieldInfo(fieldName, new DatabaseFieldInfo
            {
                FieldName = fieldName,
                TableName = _baseTableName,
                DataConvertor = convertor
            });
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(IEnumerable<IFilterParameter> filterParameters = null, IEnumerable<ISortOption> sortOptions = null, IPagingOption pagingOption = null, CancellationToken cancellationToken = default , Operator @operator = Operator.And)
        {
            return await GetAsync(GetAllQuery, null, filterParameters, @operator, sortOptions, pagingOption, cancellationToken);
        }

        public Task<IEnumerable<TEntity>> GetAsync(IQuery query = null, CancellationToken cancellationToken = default)
        {
            return GetAsync(GetAllQuery, null, query?.FilterParameters, query?.Operator ?? Operator.And, query?.SortOptions, query?.PagingOption, cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(string baseCommandText, IEnumerable<DatabaseParameter> baseParameters = null, IEnumerable<IFilterParameter> filterParameters = null, Operator @operator = Operator.And, IEnumerable<ISortOption> sortOptions = null, IPagingOption pagingOption = null, CancellationToken cancellationToken = default)
        {
            using (var command = new DataCommand(ConnectionString.ConnectionString))
            {
                if (pagingOption != null && pagingOption.Count == 0)
                    return new TEntity[0];

                var sorts = new List<ISortOption>();
                if (sortOptions != null)
                    sorts.AddRange(sortOptions);

                if (sorts.Count == 0)
                    sorts.Add(new SortOption("Id", SortOrder.Ascending));

                var list = new List<TEntity>();

                var commandText = baseCommandText;

                if (baseParameters != null)
                {
                    foreach (var databaseParameter in baseParameters)
                        command.AddParameter(databaseParameter.Name, databaseParameter.Value);
                }

                DatabaseParameter[] databaseParameters;
                commandText = AppendWhereClauseCommandText(filterParameters, @operator, out databaseParameters, commandText);
                foreach (var databaseParameter in databaseParameters)
                {
                    command.AddParameter(databaseParameter.Name, databaseParameter.Value);
                }

                string sortString;
                if (GetSortClauseString(sorts, out sortString))
                    commandText = $"{commandText} ORDER BY {sortString}";

                if (pagingOption != null)
                {
                    commandText = $"{commandText} OFFSET {pagingOption.Skip} ROWS FETCH NEXT {pagingOption.Count} ROWS ONLY";
                }

                command.CommandText = commandText;

                using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                    while (await reader.ReadAsync(cancellationToken))
                        list.Add(ReadEntity(reader));

                return list;
            }
        }

        public virtual List<IFilterParameter> GetExtraFilters(IEnumerable<IFilterParameter> filterParameters)
        {
            return new List<IFilterParameter>();
        }

        public virtual async Task<int> GetCountAsync(IEnumerable<IFilterParameter> filterParameters, CancellationToken cancellationToken, Operator @operator)
        {
            return await GetCountAsync(GetCountQuery, null, filterParameters, @operator, cancellationToken);
        }

        public Task<int> GetCountAsync(IQuery query = null, CancellationToken cancellationToken = default)
        {
            return GetCountAsync(GetCountQuery, null, query?.FilterParameters, query?.Operator ?? Operator.And, cancellationToken);
        }

        public virtual async Task<int> GetCountAsync(string baseCommandText, IEnumerable<DatabaseParameter> baseParameters, IEnumerable<IFilterParameter> filterParameters, Operator @operator, CancellationToken cancellationToken = new CancellationToken())
        {
            using (var command = new DataCommand(ConnectionString.ConnectionString))
            {
                var commandText = baseCommandText;

                if (baseParameters != null)
                {
                    foreach (var databaseParameter in baseParameters)
                        command.AddParameter(databaseParameter.Name, databaseParameter.Value);
                }

                DatabaseParameter[] databaseParameters;
                commandText = AppendWhereClauseCommandText(filterParameters, @operator, out databaseParameters, commandText);

                foreach (var databaseParameter in databaseParameters)
                {
                    command.AddParameter(databaseParameter.Name, databaseParameter.Value);
                }

                command.CommandText = commandText;
                return await command.ExecuteScalarAsync<int>(cancellationToken);
            }
        }

        internal virtual string AppendWhereClauseCommandText(IEnumerable<IFilterParameter> filterParameters, Operator @operator, out DatabaseParameter[] databaseParameters, string commandText)
        {
            var outDatabaseParameters = new List<DatabaseParameter>();
            var filters = filterParameters?.ToArray();

            string userFilterString = null;
            if (filters != null)
            {
                DatabaseParameter[] userFiltersDatabaseParameters;
                if (GetWhereClauseString(filters, @operator, out userFilterString, out userFiltersDatabaseParameters))
                {
                    outDatabaseParameters.AddRange(userFiltersDatabaseParameters);
                }
            }

            var extraFilters = GetExtraFilters(filters);
            string extraFiltersString = null;
            if (extraFilters != null)
            {
                DatabaseParameter[] extraFiltersDatabaseParameters;
                if (GetWhereClauseString(extraFilters, Operator.And, out extraFiltersString, out extraFiltersDatabaseParameters))
                {
                    outDatabaseParameters.AddRange(extraFiltersDatabaseParameters);
                }
            }

            if (!string.IsNullOrWhiteSpace(userFilterString) && !string.IsNullOrWhiteSpace(extraFiltersString))
            {
                commandText = $"{commandText} WHERE ({userFilterString}) AND ({extraFiltersString})";
            }
            else if (!string.IsNullOrWhiteSpace(userFilterString))
            {
                commandText = $"{commandText} WHERE {userFilterString}";
            }
            else if (!string.IsNullOrWhiteSpace(extraFiltersString))
            {
                commandText = $"{commandText} WHERE {extraFiltersString}";
            }

            databaseParameters = outDatabaseParameters.ToArray();
            return commandText;
        }

        internal virtual bool GetSortClauseString(IEnumerable<ISortOption> sortOptions, out string sortClauseString)
        {
            sortClauseString = null;
            if (sortOptions == null)
                return false;

            var sortStringBuilder = new StringBuilder();
            foreach (var sortOption in sortOptions)
            {
                var tableName = _baseTableName;
                var fieldName = sortOption.Key;
                var sqlCastDataType = string.Empty;

                IDatabaseFieldInfo fieldInfo;
                if (_complexFields.TryGetValue(sortOption.Key, out fieldInfo))
                {
                    if (!string.IsNullOrWhiteSpace(fieldInfo.TableName))
                        tableName = fieldInfo.TableName;

                    if (!string.IsNullOrWhiteSpace(fieldInfo.FieldName))
                        fieldName = fieldInfo.FieldName;

                    if (!string.IsNullOrWhiteSpace(fieldInfo.SqlCastDataType))
                        sqlCastDataType = fieldInfo.SqlCastDataType;
                }

                if (sortStringBuilder.Length > 0)
                    sortStringBuilder.Append(", ");

                if (string.IsNullOrWhiteSpace(sqlCastDataType))
                {
                    if (sortOption.Order == SortOrder.Ascending)
                        sortStringBuilder.AppendFormat("[{0}].[{1}] ASC", tableName, fieldName);
                    else
                        sortStringBuilder.AppendFormat("[{0}].[{1}] DESC", tableName, fieldName);
                }
                else
                {
                    if (sortOption.Order == SortOrder.Ascending)
                        sortStringBuilder.AppendFormat("CAST([{0}].[{1}] AS {2}) ASC", tableName, fieldName, sqlCastDataType);
                    else
                        sortStringBuilder.AppendFormat("CAST([{0}].[{1}] AS {2}) DESC", tableName, fieldName, sqlCastDataType);
                }
            }

            if (sortStringBuilder.Length == 0)
                return false;

            sortClauseString = sortStringBuilder.ToString();

            return true;
        }

        internal virtual bool GetWhereClauseString(IEnumerable<IFilterParameter> filterParameters, Operator @operator, out string whereClauseString, out DatabaseParameter[] parameters)
        {
            parameters = null;
            whereClauseString = null;

            if (filterParameters == null)
                return false;

            var parameterCollection = new List<DatabaseParameter>();
            var parameterCountList = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var whereStringBuilder = new StringBuilder();

            foreach (var filterParameter in filterParameters)
            {
                var tableName = _baseTableName;
                var fieldName = filterParameter.FieldName;
                var sqlCastDataType = string.Empty;
                IFilterDataConvertor valueConvertor = new GenericFilterDataConvertor();

                IDatabaseFieldInfo fieldInfo;
                if (_complexFields.TryGetValue(filterParameter.FieldName, out fieldInfo))
                {
                    if (!string.IsNullOrWhiteSpace(fieldInfo.TableName))
                        tableName = fieldInfo.TableName;

                    if (!string.IsNullOrWhiteSpace(fieldInfo.FieldName))
                        fieldName = fieldInfo.FieldName;

                    if (fieldInfo.DataConvertor != null)
                        valueConvertor = fieldInfo.DataConvertor;

                    if (!string.IsNullOrWhiteSpace(fieldInfo.SqlCastDataType))
                        sqlCastDataType = fieldInfo.SqlCastDataType;
                }

                var fieldCompleteName = string.IsNullOrWhiteSpace(sqlCastDataType) ? $"[{tableName}].[{fieldName}]" : $"CAST([{tableName}].[{fieldName}] AS {sqlCastDataType})";
                string sqlParameterName;

                var parameterKey = $"{tableName}-{fieldName}";
                int similarParameterCount = 0;
                if (parameterCountList.TryGetValue(parameterKey, out similarParameterCount))
                {
                    sqlParameterName = $"@Paramter_{tableName}_{fieldName}";
                    parameterCountList[parameterKey] = 1;
                }
                else
                {
                    sqlParameterName = $"@Paramter_{tableName}_{fieldName}_{similarParameterCount + 1}";
                    parameterCountList[parameterKey] = similarParameterCount + 1;
                }

                switch (filterParameter.Operator)
                {
                    case FilterOperatorType.Equal:
                        AddWhereString(whereStringBuilder, $"({fieldCompleteName} = {sqlParameterName})", @operator);
                        parameterCollection.Add(new DatabaseParameter(sqlParameterName, valueConvertor.ConvertToDatabaseValue(filterParameter.Values[0])));
                        break;

                    case FilterOperatorType.NotEqual:
                        AddWhereString(whereStringBuilder, $"({fieldCompleteName} <> {sqlParameterName})", @operator);
                        parameterCollection.Add(new DatabaseParameter(sqlParameterName, valueConvertor.ConvertToDatabaseValue(filterParameter.Values[0])));
                        break;

                    case FilterOperatorType.LessThan:
                        AddWhereString(whereStringBuilder, $"({fieldCompleteName} < {sqlParameterName})", @operator);
                        parameterCollection.Add(new DatabaseParameter(sqlParameterName, valueConvertor.ConvertToDatabaseValue(filterParameter.Values[0])));
                        break;

                    case FilterOperatorType.LessOrEqual:
                        AddWhereString(whereStringBuilder, $"({fieldCompleteName} <= {sqlParameterName})", @operator);
                        parameterCollection.Add(new DatabaseParameter(sqlParameterName, valueConvertor.ConvertToDatabaseValue(filterParameter.Values[0])));
                        break;

                    case FilterOperatorType.GreaterThan:
                        AddWhereString(whereStringBuilder, $"({fieldCompleteName} > {sqlParameterName})", @operator);
                        parameterCollection.Add(new DatabaseParameter(sqlParameterName, valueConvertor.ConvertToDatabaseValue(filterParameter.Values[0])));
                        break;

                    case FilterOperatorType.GreaterOrEqual:
                        AddWhereString(whereStringBuilder, $"({fieldCompleteName} >= {sqlParameterName})", @operator);
                        parameterCollection.Add(new DatabaseParameter(sqlParameterName, valueConvertor.ConvertToDatabaseValue(filterParameter.Values[0])));
                        break;

                    case FilterOperatorType.In:

                        AddWhereString(whereStringBuilder, $"({fieldCompleteName} IN ({filterParameter.Values.Select((v, i) => { var paramName = sqlParameterName + "_" + i; parameterCollection.Add(new DatabaseParameter(paramName, valueConvertor.ConvertToDatabaseValue(v))); return paramName; }).Aggregate((s, t) => string.Join(",", s, t))}))", @operator);
                        break;

                    case FilterOperatorType.NotIn:
                        AddWhereString(whereStringBuilder, $"({fieldCompleteName} NOT IN ({filterParameter.Values.Select((v, i) => { var paramName = sqlParameterName + "_" + i; parameterCollection.Add(new DatabaseParameter(paramName, valueConvertor.ConvertToDatabaseValue(v))); return paramName; }).Aggregate((s, t) => string.Join(",", s, t))}))", @operator);
                        break;

                    case FilterOperatorType.Like:
                        AddWhereString(whereStringBuilder, $"({fieldCompleteName} LIKE {sqlParameterName})", @operator);
                        parameterCollection.Add(new DatabaseParameter(sqlParameterName, valueConvertor.ConvertToDatabaseValue(filterParameter.Values[0])));
                        break;

                    case FilterOperatorType.NotLike:
                        AddWhereString(whereStringBuilder, $"({fieldCompleteName} NOT LIKE {sqlParameterName})", @operator);
                        parameterCollection.Add(new DatabaseParameter(sqlParameterName, valueConvertor.ConvertToDatabaseValue(filterParameter.Values[0])));
                        break;

                    case FilterOperatorType.Between:

                        var value1 = filterParameter.Values[0];
                        var value2 = filterParameter.Values[1];

                        if (value1 is IComparable && ((IComparable)value1).CompareTo(value2) > 0)
                        {
                            var temp = value2;
                            value2 = value1;
                            value1 = temp;
                        }

                        var sqlParameter1 = $"@Paramter_{filterParameter.FieldName}_1";
                        var sqlParameter2 = $"@Paramter_{filterParameter.FieldName}_2";

                        AddWhereString(whereStringBuilder, $"({fieldCompleteName} BETWEEN {sqlParameter1} AND {sqlParameter2})", @operator);
                        parameterCollection.Add(new DatabaseParameter(sqlParameter1, valueConvertor.ConvertToDatabaseValue(value1)));
                        parameterCollection.Add(new DatabaseParameter(sqlParameter2, valueConvertor.ConvertToDatabaseValue(value2)));

                        break;

                    case FilterOperatorType.IsNull:
                        AddWhereString(whereStringBuilder, $"({fieldCompleteName} IS NULL)", @operator);
                        break;

                    case FilterOperatorType.IsNotNull:
                        AddWhereString(whereStringBuilder, $"({fieldCompleteName} IS NOT NULL)", @operator);
                        break;
                }
            }

            if (whereStringBuilder.Length == 0)
                return false;

            parameters = parameterCollection.ToArray();
            whereClauseString = whereStringBuilder.ToString();

            return true;
        }

        private void AddWhereString(StringBuilder whereClauseString, string whereString, Operator @operator)
        {
            var operatorText = @operator == Operator.And ? "AND" : "OR";

            if (whereClauseString.Length > 0)
                whereClauseString.Append($" {operatorText} ");

            whereClauseString.Append(whereString);
        }
    }
}
