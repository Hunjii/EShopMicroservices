using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrdersPagination
{
    public record GetOrdersPaginationQuery(PaginationRequest PaginationRequest) : IQuery<GetOrdersPaginationResult>;
    public record GetOrdersPaginationResult(PaginatedResult<OrderDto> Orders);
}
