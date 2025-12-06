using Application.Abstractions;
using Application.Contracts.CardValidity;
using MediatR;

namespace Application.Features.CardValidity
{
    internal class CardValidityQueryHandler : IRequestHandler<CardValidityQuery, CardValidityResponse>
    {
        private readonly ICardProvider _cardProvider;

        public CardValidityQueryHandler(ICardProvider cardProvider)
        {
            _cardProvider = cardProvider;
        }

        public async Task<CardValidityResponse> Handle(CardValidityQuery request, CancellationToken cancellationToken)
        {
            var stateDescriptionTask = _cardProvider.GetCardStatusDescriptionAsync(request.CardNumber);
            var validityTask = _cardProvider.GetCardValidityAsync(request.CardNumber);

            await Task.WhenAll(stateDescriptionTask, validityTask);

            return new CardValidityResponse
            {
                StateDescription = stateDescriptionTask.Result,
                ValidityEnd = validityTask.Result
            };
        }
    }
}