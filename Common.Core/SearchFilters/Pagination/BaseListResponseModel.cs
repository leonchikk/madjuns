namespace Common.Core.SearchFilters.Pagination
{
    public class BaseListResponseModel<T>
    {
        public int TotalCount { get; set; }

        public T[] Data { get; set; }
    }
}
