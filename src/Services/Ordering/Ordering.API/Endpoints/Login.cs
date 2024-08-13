
using BuildingBlocks.CQRS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Authentication.Command.Login;
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints
{
    public record LoginRequest(string Email);
    public record LoginResponse(bool IsSucess, string Token);

    public record TestResult(IEnumerable<OrderDto> Orders);

    [Route("api/login")]
    public class LoginController(ISender sender) : Controller
    {
        [HttpPost("")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var command = loginRequest.Adapt<LoginCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<LoginResponse>();

            return Ok(response);
        }

        [Authorize]
        [HttpGet("test/{id}")]
        public async Task<IActionResult> Test(Guid id)
        {
            var result = await sender.Send(new GetOrdersByCustomerQuery(id));

            var response = result.Adapt<TestResult>();

            return Ok(response);
        }
    }
}
