using CardToken.Domain.TokenGeneration;
using NUnit.Framework;

namespace CardToken.Tests.Domain
{
    [TestFixture]
    public class ArrayRotationTests
    {
        [Test]
        public void Should_rotate_an_integer_array_n_times()
        {
            var numberOfRotations = 9;
            var originalArray = new[] { 1, 2, 3, 4, 5, 6 };
            var expectedArray = new[] { 4, 5, 6, 1, 2, 3 };

            var rotatedArray = ArrayRotation.RotateArray(originalArray, numberOfRotations);

            Assert.AreEqual(expectedArray, rotatedArray);
        }
    }
}