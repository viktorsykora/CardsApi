using Application.Abstractions;
using Application.Contracts.CardValidity;

namespace Application.Features.CardValidity
{
    public class CardValidityQuery : IQuery<CardValidityResponse>
    {       
        public string CardNumber { get; init; }
    }
}