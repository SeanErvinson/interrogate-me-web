using System;
using System.Text;

namespace InterrogateMe.Utilities
{
    public static class UrlHelper
    {
        #region Private Variables

        private const int Length = 7;
        private static readonly char[] _letters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
        private static readonly char[] _numbers = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

        #endregion

        #region Helper Methods

        public static string GenerateUrl()
        {
            var sb = new StringBuilder();
            var random = new Random();
            for (int i = 0; i < Length; i++)
            {
                var randomNumber = random.Next(0, 3) + 1;
                if (randomNumber == 1)
                {
                    var index = random.Next(0, _numbers.Length);
                    sb.Append(_numbers[index]);
                }
                else
                {
                    var index = random.Next(0, _letters.Length);
                    sb.Append(_letters[index]);
                }
            }
            return sb.ToString();
        }

        #endregion
    }
}
