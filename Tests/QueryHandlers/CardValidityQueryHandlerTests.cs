using Application.Abstractions;
using Application.Features.CardValidity;
using Moq;

namespace Tests.QueryHandlers
{
    public class CardValidityQueryHandlerTests
    {
        private readonly Mock<ICardProvider> _mockClient;
        private readonly CardValidityQueryHandler _handler;

        public CardValidityQueryHandlerTests()
        {
            _mockClient = new Mock<ICardProvider>();
            _handler = new CardValidityQueryHandler(_mockClient.Object);
        }

        [Fact]
        public async Task ShouldReturnCompleteDataWhenCallsSucceed()
        {
            var cardNumber = "123456";
            var stateDescription = "Active";

            var query = new CardValidityQuery { CardNumber = cardNumber  };
            var expectedDate = new DateTime(2030, 12, 31);

            _mockClient.Setup(x => x.GetCardStatusDescriptionAsync(cardNumber)).ReturnsAsync(stateDescription);
            _mockClient.Setup(x => x.GetCardValidityAsync(cardNumber)).ReturnsAsync(expectedDate);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Equal(stateDescription, result.StateDescription);
            Assert.Equal(expectedDate, result.ValidityEnd);
        }        
    }
}