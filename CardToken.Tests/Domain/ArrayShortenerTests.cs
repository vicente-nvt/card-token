using CardToken.Domain.TokenGeneration;
using NUnit.Framework;

namespace CardToken.Tests.Domain
{
    [TestFixture]
    class ArrayShortenerTests
    {
        [TestCase(new[] { 1, 9, 1, 3, 4, 4, 5, 6, 5, 8, 8, 8, 7, 7 }, new[] { 9, 5, 6, 5, 8, 8, 8, 7, 7 }, 4)]
        [TestCase(new[] { 1, 9, 1, 3, 4, 4, 5, 6, 5 }, new[] { 1, 1, 3, 4, 4, 5, 5 }, 4)]
        [TestCase(new[] { 4, 6, 5, 3, 3, 1 }, new[] { 4, 5, 3, 3, 1 }, 4)]
        [TestCase(new[] { 4, 6, 5, 3, 3, 1 }, new[] { 4, 6, 5, 3, 3 }, 3)]
        public void Should_find_the_biggest_array_considering_absolute_maximum_difference(int[] givenArray,
            int[] expectedArray, int absoluteDifference)
        {
            var calculatedArray = ArrayShortener.GetByAbsoluteDifference(givenArray, absoluteDifference);

            Assert.AreEqual(expectedArray, calculatedArray);
        }
    }
}
