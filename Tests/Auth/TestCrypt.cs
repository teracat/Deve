﻿using Xunit;
using Deve.Auth;
using System.Security.Cryptography;

namespace Deve.Tests.Auth
{
    /// <summary>
    /// Crypt Tests.
    /// </summary>
    public class TestCrypt
    {
        [Fact]
        public void Encrypt_Null_ReturnsNull()
        {
            var auth = AuthFactory.Get();

            var encrypted = auth.Crypt.Encrypt(null);

            Assert.Null(encrypted);
        }

        [Fact]
        public void Encrypt_Empty_ReturnsEmpty()
        {
            var auth = AuthFactory.Get();

            var encrypted = auth.Crypt.Encrypt(string.Empty);

            Assert.Empty(encrypted);
        }

        [Fact]
        public void Encrypt_Valid_Equal()
        {
            var auth = AuthFactory.Get();
            
            var encrypted = auth.Crypt.Encrypt("Original Text");
            System.Diagnostics.Debug.WriteLine(encrypted);

            Assert.Equal("yX0oNH+mCQ0P+vP2Qe9Tug==", encrypted);
        }

        [Fact]
        public void Decrypt_Null_ReturnsNull()
        {
            var auth = AuthFactory.Get();

            var decrypted = auth.Crypt.Decrypt(null);

            Assert.Null(decrypted);
        }

        [Fact]
        public void Decrypt_Empty_ReturnsEmpty()
        {
            var auth = AuthFactory.Get();

            var decrypted = auth.Crypt.Decrypt(string.Empty);

            Assert.Empty(decrypted);
        }

        [Fact]
        public void Decrypt_NotValid_ThrowsException()
        {
            var auth = AuthFactory.Get();

            Assert.Throws<CryptographicException>(() => auth.Crypt.Decrypt("aaaa"));
        }

        [Fact]
        public void Decrypt_Valid_Equal()
        {
            var auth = AuthFactory.Get();

            var decrypted = auth.Crypt.Decrypt("yX0oNH+mCQ0P+vP2Qe9Tug==");

            Assert.Equal("Original Text", decrypted);
        }
    }
}
