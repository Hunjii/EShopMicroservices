namespace Ordering.Application.Abstractions
{
    public interface IJwtProvider
    {
        string GetJwtToken(Customer customer);
    }
}
