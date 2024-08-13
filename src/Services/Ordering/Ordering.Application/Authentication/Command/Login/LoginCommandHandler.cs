
using Ordering.Application.Abstractions;

namespace Ordering.Application.Authentication.Command.Login
{
    internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResult>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IJwtProvider _jwtProvider;

        public LoginCommandHandler(IApplicationDbContext dbContext, IJwtProvider jwtProvider)
        {
            _dbContext = dbContext;
            _jwtProvider = jwtProvider;
        }

        public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customers.Where(c => c.Email == request.Email).FirstOrDefaultAsync();

            if (customer == null)
            {
                return new LoginResult(false, string.Empty);
            }

            string token = _jwtProvider.GetJwtToken(customer);

            return new LoginResult(true, token);
        }
    }
}
