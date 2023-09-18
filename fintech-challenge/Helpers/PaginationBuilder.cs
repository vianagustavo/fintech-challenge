namespace FintechChallenge.Helpers
{
    public class PaginationHelper<T>
    {
        public PaginatedResult<T> Paginate(IEnumerable<T> data, string dataName, int take, int skip)
        {
            int currentPage = (skip / take) + 1;

            var pagedData = data
            .Skip(skip)
            .Take(take)
            .ToList();

            var paginationInfo = new PaginationInfo
            {
                ItemsPerPage = pagedData.Count,
                CurrentPage = currentPage
            };

            return new PaginatedResult<T>(dataName, pagedData, paginationInfo);
        }
    }
    public class PaginatedResult<T>
    {
        public Dictionary<string, IEnumerable<T>> Data { get; set; }
        public PaginationInfo Pagination { get; set; }

        public PaginatedResult(string propertyName, IEnumerable<T> items, PaginationInfo pagination)
        {
            Data = new Dictionary<string, IEnumerable<T>>
            {
                [propertyName] = items
            };
            Pagination = pagination;
        }
    }

    public class PaginationInfo
    {
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
    }
}