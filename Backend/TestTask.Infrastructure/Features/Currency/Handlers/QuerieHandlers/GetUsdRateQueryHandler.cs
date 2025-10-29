using MediatR;
using System.Text.Json;
using TestTask.Infrastructure.Features.Currency.Queries;

namespace TestTask.Infrastructure.Features.Currency.Handlers.QuerieHandlers
{
    public class GetUsdRateQueryHandler : IRequestHandler<GetUsdRateQuery, decimal>
    {
        private readonly HttpClient _httpClient;

        public GetUsdRateQueryHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> Handle(GetUsdRateQuery request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetStringAsync(
                "https://api.nbrb.by/exrates/rates/431",
                cancellationToken);

            var usdRate = JsonDocument.Parse(response)
                                      .RootElement
                                      .GetProperty("Cur_OfficialRate")
                                      .GetDecimal();

            var amountInUsd = Math.Round(request.AmountInByn / usdRate, 2);
            return amountInUsd;
        }
    }
}
