using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrdersPagination;

namespace Ordering.API.Endpoints
{
    public record GetOrdersPaginationReponse(PaginatedResult<OrderDto> Orders);
    public class GetOrdersPagination : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersPaginationQuery(request));

                var response = result.Adapt<GetOrdersPaginationReponse>();

                return Results.Ok(response);
            })
            .WithName("GetOrdersPagination")
            .Produces<GetOrdersPaginationReponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Orders Pagination")
            .WithDescription("Get Orders Pagination");
        }
    }
}
