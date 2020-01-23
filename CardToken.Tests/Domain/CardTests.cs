using CardToken.Domain;
using CardToken.Domain.TokenGeneration;
using NUnit.Framework;
using System;

namespace CardToken.Tests.Domain
{
    [TestFixture]
    public class CardTests
    {
        private const string _cardNumber = "5544663388559988";
        private const string _cvv = "78877";

        [Test]
        public void Should_create_a_card_properly()
        {
            var card = new Card(_cardNumber, _cvv);

            Assert.AreEqual(_cardNumber, card.Number);
            Assert.AreEqual(_cvv, card.Cvv);
            Assert.That(DateTime.UtcNow, Is.EqualTo(card.RegistrationDateTime).Within(1).Seconds);
        }

        [Test]
        public void Should_create_a_card_with_token() {
            var date = DateTime.UtcNow;
            var dataForTokenGeneration = $"{_cardNumber}{date.Year.ToString()}{date.Month.ToString("d2")}" +
                $"{date.Day.ToString("d2")}{date.Hour.ToString("d2")}{date.Minute.ToString("d2")}";
            var absoluteDifference = 5;
            var numberOfRotations = int.Parse(_cvv);
            var expectedToken = TokenGenerator.Generate(dataForTokenGeneration, numberOfRotations, absoluteDifference);

            var card = new Card(_cardNumber, _cvv);

            Assert.AreEqual(expectedToken, card.Token);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        [TestCase("55AAA777778888BB")]
        [TestCase("55446633885599880")]
        public void Should_not_create_a_card_with_an_invalid_number(string invalidNumber)
        {
            TestDelegate act = () => new Card(invalidNumber, _cvv);

            Assert.Throws<DomainException>(act, "Invalid card number");
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        [TestCase("678990")]
        [TestCase("678Z0")]
        public void Should_not_create_a_card_with_an_invalid_cvv(string invalidCvv)
        {
            TestDelegate act = () => new Card(_cardNumber, invalidCvv);

            Assert.Throws<DomainException>(act, "Invalid CVV");
        }

        [Test]
        public void Should_check_that_a_token_is_valid()
        {
            var card = new Card(_cardNumber, _cvv);
            var registrationDate = card.RegistrationDateTime;
            var token = card.Token;

            var tokenIsValid = card.CheckIfTokenIsValid(token, int.Parse(_cvv), registrationDate);

            Assert.IsTrue(tokenIsValid);
        }

        [Test]
        public void Should_check_that_a_token_is_invalid()
        {
            var card = new Card(_cardNumber, _cvv);
            var registrationDate = DateTime.UtcNow.AddMinutes(-14);
            var token = card.Token;

            var tokenIsValid = card.CheckIfTokenIsValid(token, int.Parse(_cvv), registrationDate);

            Assert.IsFalse(tokenIsValid);
        }
    }
}