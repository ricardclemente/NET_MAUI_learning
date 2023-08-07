using System.Text;

namespace Core;
public static class PhoneWordTranslator
{
    public static string toNumber(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return null;

        raw = raw.ToUpperInvariant();

        var newNumber = new StringBuilder();
        foreach ( char c in raw)
        {
            if (" -0123456789".Contains(c))
            {
                newNumber.Append(c);
            }
            else
            {
                var result = translateToNumber(c);
                if (result != null) { newNumber.Append(result);}
                else { return null; }
            }
        }
        return newNumber.ToString();
    }

    static bool Contains(this string keyString, char c)
    {
        return keyString.IndexOf(c) >= 0;
    }

    static readonly string[] digits =
    {
        "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ"
    };

    static int? translateToNumber(char c)
    {
        for (int i=0; i < digits.Length; i++)
        {
            if (digits[i].Contains(c)) { return 2 + i; }
        }
        return null;
    }
}

