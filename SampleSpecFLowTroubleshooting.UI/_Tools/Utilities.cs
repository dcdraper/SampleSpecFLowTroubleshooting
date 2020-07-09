using System;

namespace SampleSpecFLowTroubleshooting.UI
{
    public class Utilities
    {
        public static bool AssertAreEqual(string actual, string expected, string description, bool ignoreCase = false, bool ignoreLeadingZero = false)
        {
            if (ignoreLeadingZero)
            {
                expected = expected.TrimStart('0');
                actual = actual.TrimStart('0');
            }
            if (ignoreCase)
            {
                if (string.Equals(actual, expected, StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }
            else
            {
                if (actual == expected)
                {
                    return true;
                }
            }

            Console.WriteLine();
            Console.WriteLine($"STRING VERIFICATION FAILED: {description}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Actual: {actual}");
            return false;
        }

        public static bool AssertAreEqual(bool actual, bool expected, string description)
        {
            if (actual == expected)
            {
                return true;
            }

            Console.WriteLine();
            Console.WriteLine($"BOOLEAN VERIFICATION FAILED: {description}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Actual: {actual}");
            return false;
        }

        public static bool AssertTrue(bool actual, string description)
        {
            return AssertAreEqual(actual, true, description);
        }

        public static String GetRandomString(int length)
        {
            var allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789 ,./?;:-+)(*&$#@";
            var chars = new char[length];
            var rd = new Random();
            for (var i = 0; i < length; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }
            return new string(chars);
        }
    }
}

