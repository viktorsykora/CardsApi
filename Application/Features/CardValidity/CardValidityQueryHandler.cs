using Application.Abstractions;
using Application.Contracts;
using MediatR;

namespace Application.Features.CardValidity
{
    internal class CardValidityQueryHandler : IRequestHandler<CardValidityQuery, CardValidityResponse>
    {
        private readonly ICardProvider _cardApiClient;

        public CardValidityQueryHandler(ICardProvider cardApiClient)
        {
            _cardApiClient = cardApiClient;
        }

        public async Task<CardValidityResponse> Handle(CardValidityQuery request, CancellationToken cancellationToken)
        {
            var stateDescriptionTask = _cardApiClient.GetCardStatusDescriptionAsync(request.CardNumber);
            var validityTask = _cardApiClient.GetCardValidityAsync(request.CardNumber);

            await Task.WhenAll(stateDescriptionTask, validityTask);

            return new CardValidityResponse
            {
                StateDescription = stateDescriptionTask.Result,
                ValidityEnd = validityTask.Result
            };
        }
    }
}