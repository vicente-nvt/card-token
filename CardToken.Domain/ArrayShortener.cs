using System;
using System.Collections.Generic;

namespace CardToken.Domain
{
    public static class ArrayShortener
    {
        public static int[] GetByAbsoluteDifference(int[] givenArray, int absoluteDifference)
        {
            var biggestNumber = GetBiggestNumber(givenArray);
            var smallestNumber = GetSmallestNumber(givenArray);

            var arrayFromBiggestNumber = GetArrayFromBiggestNumber(givenArray, biggestNumber - (int)absoluteDifference);
            var arrayFromSmallestNumber = GetArrayFromSmallestNumber(givenArray, smallestNumber + absoluteDifference);

            return arrayFromBiggestNumber.Length > arrayFromSmallestNumber.Length
                ? arrayFromBiggestNumber
                : arrayFromSmallestNumber;
        }

        private static int[] GetArrayFromSmallestNumber(int[] givenArray, int limit)
        {
            List<int> listOfItems = new List<int>();
            for (var i = 0; i < givenArray.Length; i++)
            {
                if (givenArray[i] <= limit)
                    listOfItems.Add(givenArray[i]);
            }
            return listOfItems.ToArray();
        }

        private static int[] GetArrayFromBiggestNumber(int[] givenArray, int limit)
        {
            List<int> listOfItems = new List<int>();
            for (var i = 0; i < givenArray.Length; i++)
            {
                if (givenArray[i] >= limit)
                    listOfItems.Add(givenArray[i]);
            }
            return listOfItems.ToArray();
        }

        private static int GetBiggestNumber(int[] givenArray)
        {
            var biggestNumber = 0;
            for (var i = 0; i < givenArray.Length; i++) {
                if (givenArray[i] > biggestNumber)
                    biggestNumber = givenArray[i];
            }
            return biggestNumber;
        }

        private static int GetSmallestNumber(int[] givenArray)
        {
            var smallestNumber = givenArray[0];
            for (var i = 1; i < givenArray.Length; i++) {
                if (givenArray[i] < smallestNumber)
                    smallestNumber = givenArray[i];
            }
            return smallestNumber;
        }
    }
}