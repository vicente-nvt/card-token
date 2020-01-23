using CardToken.Domain.TokenGeneration;
using NUnit.Framework;

namespace CardToken.Tests.Domain
{
    [TestFixture]
    public class TokenGeneratorTests
    {
        [Test]
        public void Should_generate_a_token()
        {
            var dataForTokenGeneration = "5544663388559988202001231819";
            var numberOfRotations = 78877;
            int absoluteDifference = 5;
            var tokenExpected = "155443355202001231";

            var tokenGenerated = TokenGenerator.Generate(dataForTokenGeneration, numberOfRotations, absoluteDifference);

            Assert.AreEqual(tokenExpected, tokenGenerated);
        }
    }
}
