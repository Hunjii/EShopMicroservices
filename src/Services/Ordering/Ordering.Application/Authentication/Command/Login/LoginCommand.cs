using FluentValidation;

namespace Ordering.Application.Authentication.Command.Login
{
    public record LoginCommand(string Email) : ICommand<LoginResult>;

    public record LoginResult(bool IsSucess, string Token);

    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
        }
    }
}
