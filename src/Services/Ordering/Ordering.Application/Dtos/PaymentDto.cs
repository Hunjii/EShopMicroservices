namespace Ordering.Application.Dtos
{
    public record PaymentDto(string CardName, string CardNubmer, string Expiration, string Cvv, int PaymentMethod);
}
