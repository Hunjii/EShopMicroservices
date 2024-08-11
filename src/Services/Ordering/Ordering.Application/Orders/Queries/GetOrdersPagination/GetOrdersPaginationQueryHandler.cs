
using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrdersPagination
{
    public class GetOrdersPaginationQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersPaginationQuery, GetOrdersPaginationResult>
    {
        public async Task<GetOrdersPaginationResult> Handle(GetOrdersPaginationQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

            var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new GetOrdersPaginationResult(
                new PaginatedResult<OrderDto>(
                    pageIndex,
                    pageSize,
                    totalCount,
                    orders.ToOrderDtoList())
                );
        }
    }
}
