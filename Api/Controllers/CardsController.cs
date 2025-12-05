using System.ComponentModel.DataAnnotations;
using Application.Contracts;
using Application.Features.CardValidity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class CardsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{cardNumber}/validity")]
        [Authorize]
        [ProducesResponseType(typeof(CardValidityResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<CardValidityResponse> Get([Required] string cardNumber)
        {
            return await _mediator.Send(new CardValidityQuery() { CardNumber = cardNumber });
        }
    }
}