using System;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto;
using System.IO;
using Org.BouncyCastle.Crypto.Parameters;

public class GoogleJwtHelper
{
    public static string CreateSignedJwt(string clientEmail, string privateKey, string tokenUri, int expirySeconds = 3600)
    {
        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var header = new { alg = "RS256", typ = "JWT" };
        var payload = new
        {
            iss = clientEmail,
            scope = "https://www.googleapis.com/auth/cloud-platform",
            aud = tokenUri,
            iat = now,
            exp = now + expirySeconds
        };

        string headerEncoded = Base64UrlEncode(JsonConvert.SerializeObject(header));
        string payloadEncoded = Base64UrlEncode(JsonConvert.SerializeObject(payload));
        string unsignedJwt = headerEncoded + "." + payloadEncoded;
        string signature = CreateRsaSha256Signature(unsignedJwt, privateKey);

        return $"{unsignedJwt}.{signature}";
    }

    private static string CreateRsaSha256Signature(string input, string privateKey)
    {
        string fixedPem = privateKey.Replace("\\n", "\n");

        using var reader = new StringReader(fixedPem);
        object keyObject = new PemReader(reader).ReadObject();

        RsaPrivateCrtKeyParameters rsaParams = null;

        if (keyObject is AsymmetricCipherKeyPair pair && pair.Private is RsaPrivateCrtKeyParameters crtKey)
        {
            rsaParams = crtKey;
        }
        else if (keyObject is RsaPrivateCrtKeyParameters directKey)
        {
            rsaParams = directKey;
        }
        else
        {
            throw new Exception($"Unsupported key format: {keyObject?.GetType()}");
        }

        RSA rsa = DotNetUtilities.ToRSA(rsaParams);
        byte[] data = Encoding.UTF8.GetBytes(input);
        byte[] signature = rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        return Base64UrlEncode(signature);
    }

    private static string Base64UrlEncode(string s) => Base64UrlEncode(Encoding.UTF8.GetBytes(s));
    private static string Base64UrlEncode(byte[] b) => Convert.ToBase64String(b).Replace('+', '-').Replace('/', '_').Replace("=", "");
}