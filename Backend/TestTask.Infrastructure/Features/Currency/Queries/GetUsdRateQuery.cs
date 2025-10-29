using MediatR;

namespace TestTask.Infrastructure.Features.Currency.Queries
{
    public class GetUsdRateQuery : IRequest<decimal>
    {
        public decimal AmountInByn { get; set; }
    }
}
