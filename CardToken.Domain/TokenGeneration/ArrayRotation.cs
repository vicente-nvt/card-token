namespace CardToken.Domain.TokenGeneration
{
    public static class ArrayRotation
    {
        private static int[] ReverseArray(int[] array, int start, int end)
        {
            var resultArray = array;
            while (start < end) 
            {
                int temp = resultArray[start];
                resultArray[start] = resultArray[end];
                resultArray[end] = temp;
                start++;
                end--;
             }
            return resultArray;
        }

        public static int[] RotateArray(int[] array, int numberOfRotations)
        {
            var lastItem = array.Length - 1 ;
            var resultArray = new int[] {};
            var numberOfIterations = 0;

            while (numberOfRotations > numberOfIterations) {
                resultArray = ReverseArray(array, 0, lastItem);
                resultArray = ReverseArray(resultArray, 1, lastItem);

                numberOfIterations++;
            }

            return resultArray;
        }
    }
}