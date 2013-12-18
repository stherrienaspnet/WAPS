using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using WAPS.BookStore.WebUI.Helpers;

namespace WAPS.BookStore.WebUI.Security
{
    public class Token
    {
        public Token(string userId, string fromIP, string sessionId)
        {
            UserId = userId;
            IP = fromIP;
            SessionId = sessionId;
        }

        public string UserId { get; private set; }
        public string IP { get; private set; }
        public string SessionId { get; private set; }

        public string Encrypt()
        {
            CryptographyHelper cryptographyHelper = new CryptographyHelper();
            X509Certificate2 certificate = cryptographyHelper.GetX509Certificate("CN=WebAPI-Token");
            return cryptographyHelper.Encrypt(certificate, this.ToString());
        }

        public override string ToString()
        {
            return String.Format("UserId={0};IP={1};SessionId={2}", this.UserId, this.IP, this.SessionId);
        }

        public static Token Decrypt(string encryptedToken)
        {
            CryptographyHelper cryptographyHelper = new CryptographyHelper();
            X509Certificate2 certificate = cryptographyHelper.GetX509Certificate("CN=WebAPI-Token");
            string decrypted = cryptographyHelper.Decrypt(certificate, encryptedToken);

            //Splitting it to dictionary
            Dictionary<string, string> dictionary = decrypted.ToDictionary();
            return new Token(dictionary["UserId"], dictionary["IP"], dictionary["SessionId"]);
        }
    }
}
