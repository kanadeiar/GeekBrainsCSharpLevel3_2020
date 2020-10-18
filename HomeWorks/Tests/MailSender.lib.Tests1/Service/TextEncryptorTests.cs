using MailSender.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailSender.lib.Tests1.Service
{
    [TestClass]
    public class TextEncryptorTests
    {
        [TestMethod]
        public void Encode_ABC_to_BCD_with_key_1()
        {
            const string str = "ABC";
            const int key = 1;
            const string expectedStr = "BCD";

            var actualStr = TextEncoder.Encode(str, key);

            Assert.AreEqual(expectedStr, actualStr);
        }
        [TestMethod]
        public void Decode_BCD_to_ABC_with_key_1()
        {
            const string str = "BCD";
            const int key = 1;
            const string expectedStr = "ABC";

            var actualStr = TextEncoder.Decode(str, key);

            Assert.AreEqual(expectedStr, actualStr);
        }
    }
}
