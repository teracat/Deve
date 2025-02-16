using System.Security.Cryptography;
using Deve.Tests.Auth.Fixtures;

namespace Deve.Tests.Auth
{
    /// <summary>
    /// Crypt Tests.
    /// </summary>
    public class TestCrypt : IClassFixture<FixtureAuth>
    {
        private readonly FixtureAuth _fixtureAuth;

        public TestCrypt(FixtureAuth authFixture)
        {
            _fixtureAuth = authFixture;
        }

        [Fact]
        public void Encrypt_Null_ReturnsNull()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var encrypted = _fixtureAuth.Crypt.Encrypt(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.Null(encrypted);
        }

        [Fact]
        public void Encrypt_Empty_ReturnsEmpty()
        {
            var encrypted = _fixtureAuth.Crypt.Encrypt(string.Empty);

            Assert.Empty(encrypted);
        }

        [Fact]
        public void Encrypt_Valid_Equal()
        {
            var encrypted = _fixtureAuth.Crypt.Encrypt(TestsConstants.CryptDecryptedText);
            System.Diagnostics.Debug.WriteLine(encrypted);

            Assert.Equal(TestsConstants.CryptEncryptedText, encrypted);
        }

        [Fact]
        public void Decrypt_Null_ReturnsNull()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var decrypted = _fixtureAuth.Crypt.Decrypt(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.Null(decrypted);
        }

        [Fact]
        public void Decrypt_Empty_ReturnsEmpty()
        {
            var decrypted = _fixtureAuth.Crypt.Decrypt(string.Empty);

            Assert.Empty(decrypted);
        }

        [Fact]
        public void Decrypt_NotValid_ThrowsException()
        {
            Assert.Throws<CryptographicException>(() => _fixtureAuth.Crypt.Decrypt("aaaa"));
        }

        [Fact]
        public void Decrypt_Valid_Equal()
        {
            var decrypted = _fixtureAuth.Crypt.Decrypt(TestsConstants.CryptEncryptedText);

            Assert.Equal(TestsConstants.CryptDecryptedText, decrypted);
        }
    }
}