using System.Security.Cryptography;

namespace SchoolManagerModel.Utils;

public static class HashStringMd5
{
    public static string GetHashedString(string str)
    {
        using MD5 md5 = MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(str);
        byte[] hashBytes = MD5.HashData(inputBytes);

        return Convert.ToHexString(hashBytes);
    }
}
