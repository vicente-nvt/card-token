using CardToken.Application;
using CardToken.Application.CardValidation;
using CardToken.Common;
using CardToken.Tests.Application.Builders;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using System;
using ApplicationException = CardToken.Application.ApplicationException;

namespace CardToken.Tests.Application
{
    [TestFixture]
    public class CardValidationTests
    {
        private CardRepository _cardRepository;
        private CardValidation _cardValidation;

        [SetUp]
        public void SetUp()
        {
            _cardRepository = Substitute.For<CardRepository>();
            _cardValidation = new CardValidator(_cardRepository);
        }

        [Test]
        public void Should_validate_a_valid_card_token_and_registration_date_time()
        {
            var number = "1111333355557777";
            var cvv = "7888";
            var card = CardBuilder.New()
                .WithNumber(number)
                .WithCvv(cvv)
                .Build();
            _cardRepository.GetCardByRegistrationDate(card.RegistrationDateTime.ToStringWithMiliseconds()).Returns(card);

            var validationDto = _cardValidation.Validate(card.Token, card.Cvv, card.RegistrationDateTime.ToStringWithMiliseconds());

            Assert.IsTrue(validationDto.IsValid);
        }

        [Test]
        public void Should_validate_an_invalid_card_token()
        {
            var oneNumber = "12345678901234";
            var otherNumber = "2345678901234579";
            var oneCvv = "7888";
            var otherCvv = "7899";
            var oneCard = CardBuilder.New()
                .WithNumber(oneNumber)
                .WithCvv(oneCvv)
                .Build();
            var otherCard = CardBuilder.New()
                .WithNumber(otherNumber)
                .WithCvv(otherCvv)
                .Build();
            _cardRepository.GetCardByRegistrationDate(oneCard.RegistrationDateTime.ToStringWithMiliseconds()).Returns(otherCard);

            var validationDto = _cardValidation.Validate(oneCard.Token, oneCard.Cvv, oneCard.RegistrationDateTime.ToStringWithMiliseconds());

            Assert.IsFalse(validationDto.IsValid);
        }

        [Test]
        public void Should_validate_an_invalid_registration_date_time()
        {
            var number = "1111333355557777";
            var cvv = "7888";
            var card = CardBuilder.New()
                .WithNumber(number)
                .WithCvv(cvv)
                .Build();
            var invalidRegistrationDateTime = DateTime.UtcNow.AddMinutes(-20);

            var validationDto = _cardValidation.Validate(card.Token, card.Cvv, invalidRegistrationDateTime.ToStringWithMiliseconds());

            Assert.IsFalse(validationDto.IsValid);
        }

        [Test]
        public void Should_validate_an_non_existent_card_for_registration_date()
        {
            var number = "1111333355557777";
            var cvv = "7888";
            var card = CardBuilder.New()
                .WithNumber(number)
                .WithCvv(cvv)
                .Build();
            var registrationDateTime = DateTime.UtcNow;
            _cardRepository.GetCardByRegistrationDate(Arg.Any<string>()).ReturnsNull();

            var validationDto = _cardValidation.Validate(card.Token, card.Cvv, registrationDateTime.ToStringWithMiliseconds());

            Assert.IsFalse(validationDto.IsValid);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        [TestCase("A1234")]
        [TestCase("666666")]
        public void Should_check_if_cvv_is_valid(string invalidCvv)
        {
            var token = "1234";

            TestDelegate act = () => _cardValidation.Validate(token, invalidCvv, string.Empty);

            Assert.Throws<ApplicationException>(act, "Invalid CVV");
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        [TestCase("A2020292920202")]
        public void Should_check_if_token_is_valid(string invalidToken)
        {
            var cvv = "1234";

            TestDelegate act = () => _cardValidation.Validate(invalidToken, cvv, string.Empty);

            Assert.Throws<ApplicationException>(act, "Invalid Token");
        }
    }
}
