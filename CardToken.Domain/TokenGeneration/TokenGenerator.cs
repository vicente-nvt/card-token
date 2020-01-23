using System.Linq;

namespace CardToken.Domain.TokenGeneration
{
    public class TokenGenerator
    {
        public static string Generate(string dataForTokenGeneration, int numberOfRotations, int absoluteDifference)
        {
            var array = dataForTokenGeneration.ToCharArray()
                .Select(item => int.Parse(item.ToString()))
                .ToArray();
            var shortArray = ArrayShortener.GetByAbsoluteDifference(array, absoluteDifference);
            var token = ArrayRotation.RotateArray(shortArray, numberOfRotations);

            return string.Join("", token.Select(item => item.ToString()));
        }
    }
}
