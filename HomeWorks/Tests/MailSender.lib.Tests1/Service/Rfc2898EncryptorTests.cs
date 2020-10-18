using System.Security.Cryptography;
using MailSender.Interfaces;
using MailSender.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailSender.lib.Tests1.Service
{
    [TestClass]
    public class Rfc2898EncryptorTests
    {
        private IEncryptService _encrypt = new Rfc2898Encryptor();
        [TestMethod]
        public void Encrypt_Hello_And_Decrypt_with_Password()
        {
            const string str = "Hello World!";
            const string password = "Password";

            var encryptedStr = _encrypt.Encrypt(str, password);
            var decryptedStr = _encrypt.Decrypt(encryptedStr, password);

            Assert.AreNotEqual(str, encryptedStr);
            Assert.AreEqual(str, decryptedStr);
        }
        [TestMethod]
        [ExpectedException(typeof(CryptographicException))]
        public void Wrong_Decryption_thrown_CryptographicException()
        {
            const string str = "Hello World!";
            const string password = "Password";

            var encryptedStr = _encrypt.Encrypt(str, password);
            var wrong = _encrypt.Decrypt(encryptedStr, "123");
        }
    }
}
