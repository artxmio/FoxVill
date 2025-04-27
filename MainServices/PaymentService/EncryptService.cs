using System.IO;
using System.Security.Cryptography;

namespace FoxVill.MainServices.PaymentService;

internal class EncryptService
{
    private readonly string _key = "";

    public EncryptService()
    {
        _key = KeyGenerator.Generate();
    }

    public string Encrypt(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        using var aes = Aes.Create();
        aes.Key = Convert.FromBase64String(_key);
        aes.IV = new byte[16];

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        {
            using var writer = new StreamWriter(cs);
            writer.Write(text);
        }
        return Convert.ToBase64String(ms.ToArray());
    }

    public string Decrypt(string text)
    {
        using var aes = Aes.Create();

        aes.Key = Convert.FromBase64String(_key); aes.IV = new byte[16];
        
        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using var ms = new MemoryStream(Convert.FromBase64String(text));
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var reader = new StreamReader(cs);

        return reader.ReadToEnd();
    }
}
