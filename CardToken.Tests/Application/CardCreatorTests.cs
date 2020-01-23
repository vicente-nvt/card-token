using CardToken.Application;
using CardToken.Domain;
using CardToken.Tests.Application.Builders;
using NSubstitute;
using NUnit.Framework;

namespace CardToken.Tests.Application
{
    [TestFixture]
    public class CardCreatorTests
    {
        private CardCreator _cardCreator;
        private string _cardNumber;
        private string _cvv;
        private CardRepository _cardRepository;

        [SetUp]
        public void SetUp()
        {
            _cardRepository = Substitute.For<CardRepository>();
            _cardCreator = new CardCreator(_cardRepository);
            _cardNumber = "1234567809876543";
            _cvv = "443";
        }

        [Test]
        public void Should_create_a_card()
        {
            var card = CardBuilder.New()
                .WithNumber(_cardNumber)
                .WithCvv(_cvv)
                .Build();
            var cardExpected = new CardDTO
            {
                Token = card.Token,
                RegistrationDate = card.RegistrationDate
            };

            var cardCreated = _cardCreator.CreateNewCard(_cardNumber, _cvv);

            Assert.AreEqual(cardExpected.Token, cardCreated.Token);
            Assert.That(cardExpected.RegistrationDate, Is.EqualTo(card.RegistrationDate).Within(1).Seconds);
        }

        [Test]
        public void Should_persist_the_card_during_the_creation()
        {
            var cardCreated = _cardCreator.CreateNewCard(_cardNumber, _cvv);

            _cardRepository.Received(1).Add(Arg.Is<Card>(card => card.Number == _cardNumber && card.Cvv == _cvv));
        }
    }
}
