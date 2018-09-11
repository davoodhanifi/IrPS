using System;
using System.Linq;
using System.Linq.Expressions;

namespace Noandishan.IrpsApi.Repositories.Common.QueryGenerator
{
    public class JoinInformation
    {
        private JoinInformation()
        {
        }

        public TableInformation RightTableInformation
        {
            get;
            private set;
        }

        public ColumnInformation LeftColumnInformation
        {
            get;
            private set;
        }

        public ColumnInformation RightColumnInformation
        {
            get;
            private set;
        }

        public JoinType JoinType
        {
            get;
            private set;
        }

        public TableInformation LeftTableInformation
        {
            get;
            private set;
        }

        public static JoinInformation Create<TDb1, TDb2>(Expression<Func<TDb1, object>> prop1, Expression<Func<TDb2, object>> prop2, JoinType joinType = JoinType.Inner)
        {
            var leftTableInfo = SqlGenerator.GetTableInformation<TDb1>();
            var joinTableInfo = SqlGenerator.GetTableInformation<TDb2>();
            var leftCol = FindCol(prop1);
            var rightCol = FindCol(prop2);
            return new JoinInformation
            {
                RightTableInformation = joinTableInfo,
                LeftColumnInformation = leftCol,
                RightColumnInformation = rightCol,
                LeftTableInformation = leftTableInfo,
                JoinType = joinType
            };
        }


        private static ColumnInformation FindCol<TDb>(Expression<Func<TDb, object>> propertyExpression)
        {
            var memberName = propertyExpression.GetMemberName();
            return SqlGenerator.GetTableInformation<TDb>().Columns.FirstOrDefault(q => q.PropertyName == memberName);
        }
    }
}