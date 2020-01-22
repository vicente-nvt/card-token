using System;
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
            Assert.That(DateTime.Now, Is.EqualTo(card.RegistrationData).Within(1).Seconds);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Should_not_create_a_card_without_number(string cardNumber)
        {
            TestDelegate act = () => new Card(cardNumber, _cvv);

            Assert.Throws<DomainException>(act, "Card number is missing");
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Should_not_create_a_card_without_cvv(string cvv)
        {
            TestDelegate act = () => new Card(_cardNumber, cvv);

            Assert.Throws<DomainException>(act, "CVV is missing");
        }
    }
}