using System.Security.Cryptography;
using UrlShorteningService.Manager.Services.Abstractions;

namespace UrlShorteningService.Manager.Services
{
    public class AlphanumericService : IAlphanumericService
    {
        private const string source = "012345689abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public string Create(int length)
        {
            var result = "";
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                while (result.Length != length)
                {
                    byte[] oneByte = new byte[1];
                    provider.GetBytes(oneByte);
                    char character = (char)oneByte[0];
                    if (source.Contains(character))
                    {
                        result += character;
                    }
                }
            }
            return result;
        }
    }
}
