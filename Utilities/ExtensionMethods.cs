using System.Security.Cryptography;
using System.Text;

namespace Barghto_Ticketing.Utilities;

public static class ExtensionMethods
{
    public static string ToHashPass(this string val)
    {
        var result = GetMd5Hash(val);
        result = GetSha1Hash(result);
        return result;
    }
    private static string GetMd5Hash(string input)
    {
        if (input == null)
            return null;

        var hasher = MD5.Create();

        var data = hasher.ComputeHash(Encoding.Default.GetBytes(input));

        var result = ByteArrayToString(data);
        return result;
    }
    private static string GetSha1Hash(string input)
    {
        if (input == null)
            return null;

        var hasher = SHA1.Create();

        var data = hasher.ComputeHash(Encoding.Default.GetBytes(input));

        var result = ByteArrayToString(data);
        return result;
    }
    private static string ByteArrayToString(byte[] data)
    {
        var sBuilder = new StringBuilder();

        foreach (var t in data)
            sBuilder.Append(t.ToString("x2"));

        return sBuilder.ToString();
    }
}
