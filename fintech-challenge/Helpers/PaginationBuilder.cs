namespace FintechChallenge.Helpers
{
    public class PaginatedResult<T>
    {
        public Dictionary<string, IEnumerable<T>> Items { get; set; }
        public PaginationInfo Pagination { get; set; }

        public PaginatedResult(string propertyName, IEnumerable<T> items, PaginationInfo pagination)
        {
            Items = new Dictionary<string, IEnumerable<T>>
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