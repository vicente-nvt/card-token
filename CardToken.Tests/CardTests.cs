using System;
using System.Linq;
using CardToken.Domain;
using NUnit.Framework;

namespace CardToken.Tests
{
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
            Assert.That(DateTime.UtcNow, Is.EqualTo(card.RegistrationDate).Within(1).Seconds);
        }

        [Test]
        public void Should_create_a_card_with_token() {
            var date = DateTime.UtcNow;
            var dataForTokenGeneration = $@"{_cardNumber}{date.Year.ToString()}{date.Month.ToString()}{date.Day.ToString()}{date.Hour.ToString()}{date.Minute.ToString()}";
            var arrayOfString = dataForTokenGeneration.Split("");
            var arrayOfInteger = arrayOfString.Select(item => Int32.Parse(item)).ToArray();
            var absoluteDifference = 5;
            var shortArray = ArrayShortener.GetByAbsoluteDifference(arrayOfInteger, absoluteDifference);
            var numberOfRotations = Int32.Parse(_cvv);
            var token = ArrayRotation.RotateArray(shortArray, numberOfRotations);

            var card = new Card(_cardNumber, _cvv);

            Assert.AreEqual(token, card.Token);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Should_not_create_a_card_without_number(string cardNumber)
        {
            TestDelegate act = () => new Card(cardNumber, _cvv);

            Assert.Throws<DomainException>(act, "Card number is missing");
        }

        [Test]
        public void Should_not_create_a_card_with_an_invalid_number()
        {
            var invalidNumber = "55446633885599880";

            TestDelegate act = () => new Card(invalidNumber, _cvv);

            Assert.Throws<DomainException>(act, "Invalid card number");
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Should_not_create_a_card_without_cvv(string cvv)
        {
            TestDelegate act = () => new Card(_cardNumber, cvv);

            Assert.Throws<DomainException>(act, "CVV is missing");
        }

        [Test]
        public void Should_not_create_a_card_with_an_invalid_cvv()
        {
            var invalidCvv = "678990";

            TestDelegate act = () => new Card(_cardNumber, invalidCvv);

            Assert.Throws<DomainException>(act, "Invalid CVV");
        }
    }
}