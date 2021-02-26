using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using MailSender.Interfaces;

namespace MailSender.Services
{
    /// <summary>
    /// Шифровальщик данных
    /// </summary>
    public class Rfc2898Encryptor : IEncryptService
    {
        private static readonly byte[] Salt =
            {
                0x21, 0x16, 0x15, 0x22,
                0x00, 0x12, 0xa7, 0x1f,
                0xa5, 0x99, 0xbb, 0x12,
                0x44, 0xbd, 0x23, 0x3b,
            };
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        /// <summary> Алгоритм шифровки </summary>
        private static ICryptoTransform GetAlgorithmCryptoTransform(string password)
        {
            var algorithm = Algorithm(password);
            return algorithm.CreateEncryptor();
        }
        /// <summary> Алгоритм дешифровки </summary>
        private static ICryptoTransform GetInverseAlogorithmCryptoTransform(string password)
        {
            var algorithm = Algorithm(password);
            return algorithm.CreateDecryptor();
        }
        private static Rijndael Algorithm(string password)
        {
            var pdb = new Rfc2898DeriveBytes(password, Salt);
            var algorithm = Rijndael.Create();
            algorithm.Key = pdb.GetBytes(32);
            algorithm.IV = pdb.GetBytes(16);
            return algorithm;
        }
        /// <summary> Шифровка </summary>
        public byte[] Encrypt(byte[] data, string password)
        {
            var algorithm = GetAlgorithmCryptoTransform(password);
            using (var stream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(stream, algorithm, CryptoStreamMode.Write))
            {
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();
                return stream.ToArray();
            }
        }
        /// <summary> Расшифровка </summary>
        public byte[] Decrypt(byte[] data, string password)
        {
            var algorithm = GetInverseAlogorithmCryptoTransform(password);
            using (var stream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(stream, algorithm, CryptoStreamMode.Write))
            {
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();
                return stream.ToArray();
            }
        }
        /// <summary> Шифровка строки </summary>
        public string Encrypt(string str, string password)
        {
            var encoding = Encoding ?? Encoding.UTF8;
            var bytes = encoding.GetBytes(str);
            var encryptedBytes = Encrypt(bytes, password);
            return Convert.ToBase64String(encryptedBytes);
        }
        /// <summary> Расшифровка строки </summary>
        public string Decrypt(string str, string password)
        {
            var encryptedBytes = Convert.FromBase64String(str);
            var bytes = Decrypt(encryptedBytes, password);
            var encoding = Encoding ?? Encoding.UTF8;
            return encoding.GetString(bytes);
        }
    }
}
