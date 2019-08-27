using System.Linq;

namespace Common.Core.SearchFilters
{
    public static class SearchFilterExtensions
    {
        public static IQueryable<TEntity> ApplyCustomFilter<TEntity, TSearchModel>(this IQueryable<TEntity> entities, BaseSearchFilter<TEntity, TSearchModel> filter, TSearchModel model)
        {
            return filter?.ApplyCustomFilter(entities, model) ?? entities;
        }
    }
}
