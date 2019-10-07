using Common.Core.SearchFilters.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Core.SearchFilters
{
    public class SimpleSearchModel: PagingRequestModel
    {
        public string SearchTerm { get; set; }
    }
}
