namespace BuildingBlocks.Pagination
{
    public class PaginatedResult<TEnity>(int pageIndex , int pageSize, long count, IEnumerable<TEnity> datas)
        where TEnity : class
    {
        public int PageIndex { get; } = pageIndex;
        public int PageSize { get; } = pageSize;
        public long Count { get; } = count;
        public IEnumerable<TEnity> Datas { get; } = datas;
    }
}
