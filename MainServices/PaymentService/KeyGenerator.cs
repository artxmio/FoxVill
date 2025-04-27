using System.IO;
using System.Security.Cryptography;

namespace FoxVill.MainServices.PaymentService;

internal static class KeyGenerator
{
    private static readonly string KeyFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "encryption.key");

    public static string Generate()
    {
        if (File.Exists(KeyFilePath))
        {
            return File.ReadAllText(KeyFilePath);
        }
        else
        {
            using var aes = Aes.Create();
            aes.GenerateKey();
            string key = Convert.ToBase64String(aes.Key);

            File.WriteAllText(KeyFilePath, key);

            return key;
        }
    }
}
