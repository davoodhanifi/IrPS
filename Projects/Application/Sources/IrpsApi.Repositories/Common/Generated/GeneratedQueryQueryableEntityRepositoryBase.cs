using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using IrpsApi.Framework;
using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;
using SortOrder = IrpsApi.Framework.SortOrder;

namespace Noandishan.IrpsApi.Repositories.Common.Generated
{
    public class GeneratedQueryQueryableEntityRepositoryBase<TEntity, TDb> : QueryableEntityRepositoryBase<TEntity> where TEntity : IEntity
        where TDb : EntityBase, new()
    {
        private string _getByIdQuery;
        private string _getAllQuery;
        private string _getCountQuery;

        internal static readonly TableInformation TableInfo = SqlGenerator.GetTableInformation<TDb>();

        internal virtual IEnumerable<JoinInformation> GetJoinInformations()
        {
            return new JoinInformation[0];
        }

        public GeneratedQueryQueryableEntityRepositoryBase(IConnectionString connectionString) : base(TableInfo.Name, connectionString)
        {
            Initialize();
        }

        private void Initialize()
        {
            _getByIdQuery = GenerateGetByIdQuery();
            var selectQuery = new StringBuilder(GenerateSelectQuery());
            var countQuery = new StringBuilder(GenerateCountQuery());

            foreach (var joinInfo in GetJoinInformations())
            {
                foreach (var column in joinInfo.RightTableInformation.Columns)
                {
                    AddFieldInfo($"{joinInfo.RightTableInformation.Type.Name}.{column.PropertyName}", joinInfo.RightTableInformation.Name, column.Name);
                }

                var joinStatement = SqlGenerator.JoinStatement(joinInfo);

                selectQuery.Append(" ").AppendLine(joinStatement);
                countQuery.Append(" ").AppendLine(joinStatement);
            }

            _getAllQuery = selectQuery.ToString();
            _getCountQuery = countQuery.ToString();
            
        }

        protected virtual string GenerateSelectQuery()
        {
            return SqlGenerator.SelectQuery(TableInfo);
        }

        protected virtual string GenerateCountQuery()
        {
            return SqlGenerator.CountQuery(TableInfo);
        }

        protected virtual string GenerateGetByIdQuery()
        {
            return SqlGenerator.SelectByIdQuery(TableInfo);
        }

        protected override string GetByIdQuery => _getByIdQuery;

        protected override string GetAllQuery => _getAllQuery;

        protected override string GetCountQuery => _getCountQuery;

        internal override EntityBase CreateEntity()
        {
            return new TDb();
        }

        public override Task<IEnumerable<TEntity>> GetAsync(IEnumerable<IFilterParameter> filterParameters = null, IEnumerable<ISortOption> sortOptions = null, IPagingOption pagingOption = null, CancellationToken cancellationToken = default, Operator @operator = Operator.And)
        {
            return GetAsync(GetAllQuery, null, filterParameters, @operator, sortOptions, pagingOption, cancellationToken);
        }

        public override async Task<IEnumerable<TEntity>> GetAsync(string baseCommandText, IEnumerable<DatabaseParameter> baseParameters = null, IEnumerable<IFilterParameter> filterParameters = null, Operator @operator = Operator.And, IEnumerable<ISortOption> sortOptions = null, IPagingOption pagingOption = null, CancellationToken cancellationToken = default)
        {
            if (pagingOption != null && pagingOption.Count == 0)
                return new TEntity[0];

            var sorts = new List<ISortOption>();
            if (sortOptions != null)
                sorts.AddRange(sortOptions);

            if (sorts.Count == 0)
                sorts.Add(new SortOption("Id", SortOrder.Ascending));

            using (var connection = GetConnection())
            {
                DatabaseParameter[] databaseParameters;

                var queryParameters = new DynamicParameters();
                var commandText = baseCommandText;

                if (baseParameters != null)
                {
                    foreach (var databaseParameter in baseParameters)
                        queryParameters.Add(databaseParameter.Name, databaseParameter.Value);
                }

                commandText = AppendWhereClauseCommandText(filterParameters, @operator, out databaseParameters, commandText);
                foreach (var databaseParameter in databaseParameters)
                {
                    queryParameters.Add(databaseParameter.Name, databaseParameter.Value);
                }

                string sortString;
                if (GetSortClauseString(sorts, out sortString))
                    commandText = $"{commandText} ORDER BY {sortString}";

                if (pagingOption != null)
                {
                    commandText = $"{commandText} OFFSET {pagingOption.Skip} ROWS FETCH NEXT {pagingOption.Count} ROWS ONLY";
                }

                return (await connection.QueryAsync<TDb>(new CommandDefinition(commandText, queryParameters, cancellationToken: cancellationToken))).Cast<TEntity>().ToList();
            }
        }

        public override async Task<TEntity> GetAsync(string id, CancellationToken cancellationToken = new CancellationToken())
        {
            using (var connection = GetConnection())
            {
                return (TEntity)(IEntity)(await connection.QueryAsync<TDb>(new CommandDefinition(_getByIdQuery, new
                {
                    Id = id
                }, cancellationToken: cancellationToken))).FirstOrDefault();
            }
        }

        protected virtual SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString.ConnectionString);
        }
    }
}