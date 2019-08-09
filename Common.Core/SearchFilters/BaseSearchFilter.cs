using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Core.SearchFilters
{
    public class BaseSearchFilter<TEntity, TSearchModel>
    {
        protected List<SearchFieldMutator<TEntity, TSearchModel>> SearchFieldMutators { get; set; }

        public BaseSearchFilter()
        {
            SearchFieldMutators = new List<SearchFieldMutator<TEntity, TSearchModel>>();
        }

        public IQueryable<TEntity> ApplyCustomFilter(IQueryable<TEntity> entitiesQueriable, TSearchModel searchModel)
        {
            if (searchModel == null || entitiesQueriable == null)
            {
                return entitiesQueriable;
            }
            foreach (SearchFieldMutator<TEntity, TSearchModel> searchFieldMutator in SearchFieldMutators)
            {
                entitiesQueriable = searchFieldMutator.Apply(searchModel, entitiesQueriable);
            }

            return entitiesQueriable;
        }

        protected void AddSearchFieldMutator(Predicate<TSearchModel> condition, QueryMutator<TEntity, TSearchModel> mutator)
        {
            SearchFieldMutators.Add(new SearchFieldMutator<TEntity, TSearchModel>(condition, mutator));
        }
    }
}
