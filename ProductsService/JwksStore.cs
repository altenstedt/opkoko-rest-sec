using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using TokenService;

namespace ProductsService
{
    public class JwksStore
    {
        private readonly string url;
        private readonly HttpClient client = new HttpClient();

        private readonly Lazy<IEnumerable<SecurityKey>> lazySecurityKeys;

        public JwksStore(string url) 
        {
            this.url = url;

            lazySecurityKeys = new Lazy<IEnumerable<SecurityKey>>(GetSecurityKeys);
        }

        public IEnumerable<SecurityKey> SecurityKeys => lazySecurityKeys.Value;

        public IEnumerable<SecurityKey> GetSecurityKeys() 
        {
            var response = client.GetAsync(url).GetAwaiter().GetResult();

            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var jwks = JsonConvert.DeserializeObject<JwksDataContract>(content);

            var securityKeys = new List<SecurityKey>();

            foreach (var jwk in jwks.Keys)
            {
                if (jwk.KeyType == "RSA") 
                {
                    var parameters = new RSAParameters();
                    parameters.Exponent = jwk.Exponent.FromBaseUrlSafeString();
                    parameters.Modulus = jwk.Modulus.FromBaseUrlSafeString();

                    var securityKey = new RsaSecurityKey(parameters);

                    securityKeys.Add(securityKey);
                }
            }

            return securityKeys;
        }
   }

    internal static class Base64Extensions
    {
        public static byte[] FromBaseUrlSafeString(this string value)
        {
            string text = value
                .Replace('_', '/')
                .Replace('-', '+');

            switch (value.Length % 4)
            {
                case 2:
                    return Convert.FromBase64String(text + "==");
                case 3:
                    return Convert.FromBase64String(text + "=");
            }

            return Convert.FromBase64String(text);
        }
    }
}