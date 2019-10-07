using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Core.SearchFilters.Pagination
{
    public class PagingRequestModel
    {
        public int? Limit { get; set; }

        public int? Page { get; set; }

        public PagingRequestModel()
        {
            Limit = -1;
            Page = 1;
        }
    }
}
