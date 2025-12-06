using Application.Contracts.CardValidity;
using MediatR;

namespace Application.Features.CardValidity
{
    public class CardValidityQuery : IRequest<CardValidityResponse>
    {       
        public string CardNumber { get; init; }
    }
}