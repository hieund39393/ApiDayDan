using System;
using System.Text;

namespace EVN.Core.Utility
{
    public static class StringUtilities
    {
        public static string GenerateRandomCharacter(int length)
        {
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string number = "1234567890";
            const string special = "!@#$%^&*";

            var middle = length / 2;
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                if (middle == length)
                {
                    res.Append(number[rnd.Next(number.Length)]);
                }
                else if (middle - 1 == length)
                {
                    res.Append(special[rnd.Next(special.Length)]);
                }
                else
                {
                    if (length % 2 == 0)
                    {
                        res.Append(lower[rnd.Next(lower.Length)]);
                    }
                    else
                    {
                        res.Append(upper[rnd.Next(upper.Length)]);
                    }
                }
            }
            return res.ToString();
        }

        public static string SubString(string str)
        {
            const int maxLength = 3999;
            if (string.IsNullOrWhiteSpace(str)) return string.Empty;
            else if (str.Length <= maxLength) return str;
            else return str.Substring(0, maxLength);
        }
    }
}
