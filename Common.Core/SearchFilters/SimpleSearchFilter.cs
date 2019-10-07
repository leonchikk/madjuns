using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Common.Core.SearchFilters
{
    public class SimpleSearchFilter
    {
        private static MethodInfo ContainsMethod => typeof(string).GetMethod("Contains", new[] { typeof(string) });

        public static Expression<Func<T, bool>> GetSimpleSearchExpression<T>(string[] propertyNames, string searchTerm)
        {
            var parameterExpression = Expression.Parameter(typeof(T));

            var methodCalls = new List<Expression>();
            foreach (string propertyName in propertyNames)
            {
                var nestedProperties = propertyName.Split('.').ToList();
                Expression propertyExpression = parameterExpression;
                for (int i = 0; i < nestedProperties.Count; i++)
                {
                    propertyExpression = Expression.PropertyOrField(propertyExpression, nestedProperties[i]);
                }

                var searchTermValue = Expression.Constant(searchTerm, typeof(string));
                var containsMethodExp = Expression.Call(propertyExpression, ContainsMethod, searchTermValue);
                methodCalls.Add(containsMethodExp);
            }
            var orExpression = methodCalls.Aggregate((left, right) => Expression.Or(left, right));

            return Expression.Lambda<Func<T, bool>>(orExpression, parameterExpression);
        }
    }
}
