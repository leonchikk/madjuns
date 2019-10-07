
using Common.Core.SearchFilters;
using System.Linq;

namespace Common.Core.Extensions
{
    public static class SearchFilterExtensions
    {
        public static IQueryable<TEntity> ApplyCustomFilter<TEntity, TSearchModel>(this IQueryable<TEntity> entities, BaseSearchFilter<TEntity, TSearchModel> filter, TSearchModel model)
        {
            return filter?.ApplyCustomFilter(entities, model) ?? entities;
        }

        public static IQueryable<TEntity> ApplySimpleFilter<TEntity>(this IQueryable<TEntity> entities, string searchTerm, string[] propertyNames)
        {
            return string.IsNullOrEmpty(searchTerm) ? entities : entities.Where(SimpleSearchFilter.GetSimpleSearchExpression<TEntity>(propertyNames, searchTerm));
        }
    }
}
