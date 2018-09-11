using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Noandishan.IrpsApi.Repositories.Common.QueryGenerator
{
    public static class SqlGenerator
    {
        private static readonly ConcurrentDictionary<Type, TableInformation> TableInformations = new ConcurrentDictionary<Type, TableInformation>();

        private static readonly Dictionary<Type, SqlDbType> TypeMap = new Dictionary<Type, SqlDbType>
        {
            [typeof(byte)] = SqlDbType.TinyInt,
            [typeof(sbyte)] = SqlDbType.SmallInt,
            [typeof(short)] = SqlDbType.SmallInt,
            [typeof(ushort)] = SqlDbType.Int,
            [typeof(int)] = SqlDbType.Int,
            [typeof(uint)] = SqlDbType.BigInt,
            [typeof(long)] = SqlDbType.BigInt,
            [typeof(ulong)] = SqlDbType.Decimal,
            [typeof(float)] = SqlDbType.Real,
            [typeof(double)] = SqlDbType.Float,
            [typeof(decimal)] = SqlDbType.Decimal,
            [typeof(bool)] = SqlDbType.Bit,
            [typeof(string)] = SqlDbType.NVarChar,
            [typeof(char)] = SqlDbType.NChar,
            [typeof(Guid)] = SqlDbType.UniqueIdentifier,
            [typeof(DateTime)] = SqlDbType.DateTime,
            [typeof(DateTimeOffset)] = SqlDbType.DateTimeOffset,
            [typeof(TimeSpan)] = SqlDbType.Time,
            [typeof(byte[])] = SqlDbType.VarBinary,
            [typeof(byte?)] = SqlDbType.TinyInt,
            [typeof(sbyte?)] = SqlDbType.SmallInt,
            [typeof(short?)] = SqlDbType.SmallInt,
            [typeof(ushort?)] = SqlDbType.Int,
            [typeof(int?)] = SqlDbType.Int,
            [typeof(uint?)] = SqlDbType.BigInt,
            [typeof(long?)] = SqlDbType.BigInt,
            [typeof(ulong?)] = SqlDbType.Decimal,
            [typeof(float?)] = SqlDbType.Real,
            [typeof(double?)] = SqlDbType.Float,
            [typeof(decimal?)] = SqlDbType.Decimal,
            [typeof(bool?)] = SqlDbType.Bit,
            [typeof(char?)] = SqlDbType.NChar,
            [typeof(Guid?)] = SqlDbType.UniqueIdentifier,
            [typeof(DateTime?)] = SqlDbType.DateTime,
            [typeof(DateTimeOffset?)] = SqlDbType.DateTimeOffset,
            [typeof(TimeSpan?)] = SqlDbType.Time,
            [typeof(object)] = SqlDbType.Variant
        };

        private static string GetSqlType(Type type)
        {
            if (type.IsEnum)
            {
                type = Enum.GetUnderlyingType(type);
            }

            SqlDbType sqlDbType;
            if (TypeMap.TryGetValue(type, out sqlDbType))
            {
                switch (sqlDbType)
                {
                    case SqlDbType.BigInt:
                    case SqlDbType.Int:
                    case SqlDbType.Bit:
                    case SqlDbType.DateTime:
                    case SqlDbType.SmallInt:
                    case SqlDbType.SmallDateTime:
                    case SqlDbType.TinyInt:
                    case SqlDbType.Date:
                    case SqlDbType.Time:
                    case SqlDbType.UniqueIdentifier:
                    case SqlDbType.Float:
                    case SqlDbType.Real:
                    case SqlDbType.SmallMoney:
                    case SqlDbType.Image:
                        return $"[{sqlDbType.ToString().ToUpper()}]";

                    case SqlDbType.DateTime2:
                    case SqlDbType.DateTimeOffset:
                        return $"[{sqlDbType.ToString().ToUpper()}](7)";

                    case SqlDbType.Decimal:
                    case SqlDbType.Money:
                        return $"[{sqlDbType.ToString().ToUpper()}](38, 38)";
                    case SqlDbType.Binary:
                    case SqlDbType.VarBinary:
                    case SqlDbType.Char:
                    case SqlDbType.NChar:
                    case SqlDbType.VarChar:
                    case SqlDbType.NVarChar:
                    case SqlDbType.Text:
                    case SqlDbType.NText:
                        return $"[{sqlDbType.ToString().ToUpper()}](MAX)";
                    case SqlDbType.Timestamp:
                        return "[VARBINARY](8)";
                    default:
                        throw new NotSupportedException($"SqlDbType {sqlDbType} Not Supported");
                }
            }

            throw new NotSupportedException($"Type {type.Name} Not Supported");
        }

        public static TableInformation GetTableInformation<T>(string tableName = null, string schemaName = null)
        {
            var type = typeof(T);
            TableInformation tableInformation;
            if (TableInformations.TryGetValue(type, out tableInformation))
                return tableInformation;

            var tableAttrib = type.GetCustomAttribute<TableAttribute>();
            tableName = tableName ?? tableAttrib?.Name ?? type.Name;
            schemaName = schemaName ?? tableAttrib?.Schema;

            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            PrimaryKeyAttribute primaryKeyAttrib = null;
            IdentityAttribute identityColumnAttribute = null;
            var columns = new List<ColumnInformation>();
            foreach (var propertyInfo in props)
            {
                var ignoreAttribute = propertyInfo.GetCustomAttribute<IgnoreColumnAttribute>();
                if (ignoreAttribute != null)
                    continue;

                var aliasAttribute = propertyInfo.GetCustomAttribute<ColumnAliasAttribute>();
                var columnInformation = new ColumnInformation(aliasAttribute != null ? aliasAttribute.Name : propertyInfo.Name)
                {
                    PropertyName = propertyInfo.Name
                };

                var ignoreWriteAttribute = propertyInfo.GetCustomAttribute<IgnoreWriteAttribute>();
                if (ignoreWriteAttribute != null)
                    columnInformation.Flags |= ColumnFlags.IgnoreWrite;

                var outputColumnAttribute = propertyInfo.GetCustomAttribute<OutputColumnAttribute>();
                if (outputColumnAttribute != null)
                    columnInformation.Flags |= ColumnFlags.OutputColumn;

                var sqlColumnTypeAttribute = propertyInfo.GetCustomAttribute<SqlColumnTypeAttribute>();
                if (sqlColumnTypeAttribute != null)
                    columnInformation.SqlType = sqlColumnTypeAttribute.FullType;
                else
                    columnInformation.SqlType = GetSqlType(propertyInfo.PropertyType);

                columns.Add(columnInformation);

                if (primaryKeyAttrib == null && (primaryKeyAttrib = propertyInfo.GetCustomAttribute<PrimaryKeyAttribute>()) != null)
                    columnInformation.Flags |= ColumnFlags.PrimaryKey;

                if (identityColumnAttribute == null && (identityColumnAttribute = propertyInfo.GetCustomAttribute<IdentityAttribute>()) != null)
                    columnInformation.Flags |= ColumnFlags.Identiy;
            }

            tableInformation = new TableInformation(tableName)
            {
                Columns = columns.ToArray(),
                Schema = schemaName,
                Type = type
            };

            TableInformations[type] = tableInformation;

            return tableInformation;
        }

        public static string SelectByIdQuery<T>(string tableName = null, string schemaName = null, TableInformation tableInformation = null)
        {
            var tableInfo = tableInformation ?? GetTableInformation<T>(tableName, schemaName);
            return SelectByIdQuery(tableInfo);
        }

        public static string SelectQuery<T>(string tableName = null, string schemaName = null, TableInformation tableInformation = null)
        {
            var tableInfo = tableInformation ?? GetTableInformation<T>(tableName, schemaName);
            return SelectQuery(tableInfo);
        }

        public static string SelectByIdQuery(TableInformation tableInfo)
        {
            var selectQuery = SelectQuery(tableInfo);
            return $"{selectQuery} WHERE {tableInfo.ScapedName}.{tableInfo.GetPrimaryKeyColumn().ScapedName} = @Id";
        }

        public static string SelectQuery(TableInformation tableInfo)
        {
            return $"SELECT {string.Join(", ", tableInfo.Columns.Select(colInfo => GetSelectQueryColumnStatement(tableInfo, colInfo)))} FROM {tableInfo.Identifier}";
        }

        private static string GetSelectQueryColumnStatement(TableInformation tableInfo, ColumnInformation columnInfo)
        {
            if (columnInfo.Name == columnInfo.PropertyName)
                return $"{tableInfo.ScapedName}.{columnInfo.ScapedName}";
            else
                return $"{tableInfo.ScapedName}.{columnInfo.ScapedName} AS {columnInfo.ScapedPropertyName}";
        }

        public static string CountQuery(TableInformation tableInfo)
        {
            return $"SELECT COUNT(*) FROM {tableInfo.Identifier}";
        }

        public static string InsertQuery(TableInformation tableInfo)
        {
            var sb = new StringBuilder();

            string outputClause;
            string outputSelectClause;
            var outputTableVarDeclaration = GetOutputInfo(tableInfo, out outputClause, out outputSelectClause);

            if (outputTableVarDeclaration != null)
                sb.AppendLine(outputTableVarDeclaration);

            var fields = tableInfo.Columns.Where(t => !t.Flags.HasFlag(ColumnFlags.Identiy) && !t.Flags.HasFlag(ColumnFlags.IgnoreWrite)).ToArray();
            sb.AppendLine($"INSERT INTO {tableInfo.Identifier}({string.Join(", ", fields.Select(q => q.ScapedName))})");

            if (outputClause != null)
                sb.AppendLine(outputClause);

            sb.AppendLine($"VALUES({string.Join(", ", fields.Select(q => $"@{q.PropertyName}"))})");

            if (outputSelectClause != null)
                sb.AppendLine(outputSelectClause);

            return sb.ToString();
        }

        public static string UpdateQuery<T>(string tableName = null, string schemaName = null)
        {
            return UpdateQuery(GetTableInformation<T>(tableName, schemaName));
        }
        /*
                public static string UpdateQuery(TableInformation tableInfo)
                {
                    var primaryCol = tableInfo.GetPrimaryKeyColumn();
                    var fields = tableInfo.Columns.Where(t => !t.Flags.HasFlag(ColumnFlags.Identiy) && !t.Flags.HasFlag(ColumnFlags.IgnoreWrite));
                    var sb = new StringBuilder();
                    sb.AppendLine($"UPDATE {tableInfo.Identifier} SET");
                    sb.AppendLine(string.Join(", ", fields.Select(q => $"{q.ScapedName} = @{q.PropertyName}")));
                    sb.AppendLine($"WHERE {tableInfo.ScapedName}.{primaryCol.ScapedName} = @{primaryCol.PropertyName}");

                    return sb.ToString();
                }*/

        public static string UpdateQuery(TableInformation tableInfo)
        {
            string outputClause;
            string outputSelectClause;
            var outputTableVarDeclaration = GetOutputInfo(tableInfo, out outputClause, out outputSelectClause);

            var primaryCol = tableInfo.GetPrimaryKeyColumn();
            var fields = tableInfo.Columns.Where(t => !t.Flags.HasFlag(ColumnFlags.Identiy) && !t.Flags.HasFlag(ColumnFlags.IgnoreWrite));
            var sb = new StringBuilder();

            if (outputTableVarDeclaration != null)
                sb.AppendLine(outputTableVarDeclaration);

            sb.AppendLine($"UPDATE {tableInfo.Identifier} SET");
            sb.AppendLine(string.Join(", ", fields.Select(q => $"{q.ScapedName} = @{q.PropertyName}")));

            if (outputClause != null)
                sb.AppendLine(outputClause);

            sb.AppendLine($"WHERE {tableInfo.ScapedName}.{primaryCol.ScapedName} = @{primaryCol.PropertyName}");

            if (outputSelectClause != null)
                sb.AppendLine(outputSelectClause);

            return sb.ToString();
        }

        public static string SafeUpdateQuery(TableInformation tableInfo)
        {
            string outputClause;
            string outputSelectClause;
            var outputTableVarDeclaration = GetOutputInfo(tableInfo, out outputClause, out outputSelectClause);

            var primaryCol = tableInfo.GetPrimaryKeyColumn();
            var fields = tableInfo.Columns.Where(t => !t.Flags.HasFlag(ColumnFlags.Identiy) && !t.Flags.HasFlag(ColumnFlags.IgnoreWrite));
            var sb = new StringBuilder();

            if (outputTableVarDeclaration != null)
                sb.AppendLine(outputTableVarDeclaration);

            sb.AppendLine($"UPDATE {tableInfo.Identifier} SET");
            sb.AppendLine(string.Join(", ", fields.Select(q => $"{q.ScapedName} = @{q.PropertyName}")));

            if (outputClause != null)
                sb.AppendLine(outputClause);

            sb.AppendLine($"WHERE {tableInfo.ScapedName}.{primaryCol.ScapedName} = @{primaryCol.PropertyName}");
            sb.AppendLine($"AND {tableInfo.ScapedName}.[RecordVersion] = @RecordVersion");

            if (outputSelectClause != null)
                sb.AppendLine(outputSelectClause);

            return sb.ToString();
        }

        private static string GetOutputInfo(TableInformation tableInfo, out string outputClause, out string outputSelectClause)
        {
            var outputColumns = tableInfo.Columns.Where(q => q.Flags.HasFlag(ColumnFlags.OutputColumn)).ToArray();
            string outputTableVarDeclaration = null;
            outputClause = null;
            outputSelectClause = null;
            if (outputColumns.Length > 0)
            {
                outputTableVarDeclaration = $"DECLARE @OutputTableVar TABLE({string.Join(", ", outputColumns.Select(q => $"{q.ScapedName} {q.SqlType}"))});";
                outputClause = "OUTPUT " + string.Join(", ", outputColumns.Select(q => $"[INSERTED].{q.ScapedName}")) + " INTO @OutputTableVar";
                outputSelectClause = $"SELECT {string.Join(", ", outputColumns.Select(q => $"[@OutputTableVar].{q.ScapedName}"))} FROM @OutputTableVar";
            }
            return outputTableVarDeclaration;
        }

        public static string DeleteQuery<T>(string tableName = null, string schemaName = null)
        {
            return DeleteQuery(GetTableInformation<T>(tableName, schemaName));
        }

        public static string DeleteQuery(TableInformation tableInfo)
        {
            var primaryCol = tableInfo.GetPrimaryKeyColumn();
            return $"DELETE FROM {tableInfo.Identifier} WHERE {tableInfo.ScapedName}.{primaryCol.ScapedName} = @{primaryCol.PropertyName}";
        }

        private static string GetJoinKeyword(JoinType joinType)
        {
            switch (joinType)
            {
                case JoinType.Inner:
                    return "JOIN";
                case JoinType.Left:
                    return "LEFT JOIN";
                case JoinType.Right:
                    return "RIGHT JOIN";
                default:
                    throw new ArgumentOutOfRangeException(nameof(joinType), joinType, null);
            }
        }

        public static string JoinStatement(JoinInformation joinInfo)
        {
            return $"{GetJoinKeyword(joinInfo.JoinType)} {joinInfo.RightTableInformation.Identifier} ON {joinInfo.LeftTableInformation.ScapedName}.{joinInfo.LeftColumnInformation.ScapedName} = {joinInfo.RightTableInformation.ScapedName}.{joinInfo.RightColumnInformation.ScapedName}";
        }

        public static string SelectRecordByIdQuery(TableInformation tableInfo)
        {
            return $"{SelectByIdQuery(tableInfo)} AND {tableInfo.ScapedName}.[RecordState] <> 2";
        }
    }
}