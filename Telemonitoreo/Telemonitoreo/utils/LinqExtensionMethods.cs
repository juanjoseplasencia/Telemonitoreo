using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Helpers;

namespace Telemonitoreo.Utils
{
    public static class LinqExtensionMethods
    {
        public static DataTable CopyToDataTable<T>(this IEnumerable<T> source)
        {
            return new ObjectShredder<T>().Shred(source, null, null);
        }

        public static DataTable CopyToDataTable<T>(this IEnumerable<T> source,
                                                    DataTable table, LoadOption? options)
        {
            return new ObjectShredder<T>().Shred(source, table, options);
        }
        
        public static IQueryable<T> OrderQueryBy<T>(this IQueryable<T> query, string sortExpression, string sortDirection)
        {
            string methodName;
            string SortItem;
            string SortItemName;
            bool multipleSort = false;
            string[] ArraySortItems = null;

            if (sortExpression.IndexOf(GlobalConstants.Colon) == -1)
            {
                SortItem = sortExpression;
                methodName = string.Format("OrderBy{0}", sortDirection.ToLower() == "asc" ? string.Empty : "Descending");
            }
            else
            {
                multipleSort = true;
                ArraySortItems = sortExpression.Split(GlobalConstants.ColonChar);
                SortItem = ArraySortItems[0];
                methodName = string.Format("OrderBy{0}", SortItem.IndexOf("desc") > 0 ? "Descending" : string.Empty);
            }

            SortItemName = SortItem.Replace("asc", string.Empty).Replace("desc", string.Empty).Trim();
            ParameterExpression parameter = Expression.Parameter(query.ElementType, "P");
            MemberExpression memberAccess = null;
            foreach (var property in SortItemName.Split(GlobalConstants.PeriodChar))
            {
                memberAccess = MemberExpression.Property(memberAccess ?? (parameter as Expression), property);
            }
            LambdaExpression orderByLambda = Expression.Lambda(memberAccess, parameter);
            MethodCallExpression result = Expression.Call(
                        typeof(Queryable),
                        methodName,
                        new[] { query.ElementType, memberAccess.Type },
                        query.Expression,
                        Expression.Quote(orderByLambda));

            if (multipleSort)
            {
                byte pos = 1;
                while (pos < ArraySortItems.Length)
                {
                    SortItem = ArraySortItems[pos].Trim();
                    methodName = string.Format("ThenBy{0}", SortItem.IndexOf("desc") > 0 ? "Descending" : string.Empty);
                    SortItemName = SortItem.Replace("asc", string.Empty).Replace("desc", string.Empty).Trim();
                    memberAccess = null;
                    foreach (var property in SortItemName.Split(GlobalConstants.PeriodChar))
                    {
                        memberAccess = MemberExpression.Property(memberAccess ?? (parameter as Expression), property);
                    }
                    orderByLambda = Expression.Lambda(memberAccess, parameter);
                    result = Expression.Call(
                                typeof(Queryable),
                                methodName,
                                new[] { query.ElementType, memberAccess.Type },
                                result,
                                Expression.Quote(orderByLambda));
                    pos++;
                }
            }
            return query.Provider.CreateQuery<T>(result);
        }

        public static IQueryable<T> FilterByBooleanExpression<T>(this IQueryable<T> query, string field, bool value )
        {
            ParameterExpression ParamExp = Expression.Parameter(typeof(T), "Entity");
            Expression InitialExp;
            LambdaExpression FinalExp;
            InitialExp = Expression.Equal(Expression.PropertyOrField(ParamExp, field), Expression.Constant(value));
            FinalExp = Expression.Lambda<Func<T, bool>>(InitialExp, new ParameterExpression[] { ParamExp });
            MethodCallExpression result = Expression.Call(typeof(Queryable), "Where", new Type[] { query.ElementType }, query.Expression, Expression.Quote(FinalExp));
            return query.Provider.CreateQuery<T>(result);
        }

    }
}