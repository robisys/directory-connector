﻿using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Text;

namespace Bit.Core.Utilities
{
    public static class Crypto
    {
        public static byte[] MakeKeyFromPassword(string password, string salt)
        {
            if(password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            if(salt == null)
            {
                throw new ArgumentNullException(nameof(salt));
            }

            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var saltBytes = Encoding.UTF8.GetBytes(salt);

            var keyBytes = DeriveKey(passwordBytes, saltBytes, 5000);

            password = null;
            passwordBytes = null;

            return keyBytes;
        }

        public static string MakeKeyFromPasswordBase64(string password, string salt)
        {
            var key = MakeKeyFromPassword(password, salt);
            password = null;
            return Convert.ToBase64String(key);
        }

        public static byte[] HashPassword(byte[] key, string password)
        {
            if(key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if(password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            var passwordBytes = Encoding.UTF8.GetBytes(password);

            var hashBytes = DeriveKey(key, passwordBytes, 1);

            password = null;
            key = null;

            return hashBytes;
        }

        public static string HashPasswordBase64(byte[] key, string password)
        {
            var hash = HashPassword(key, password);
            password = null;
            key = null;
            return Convert.ToBase64String(hash);
        }

        private static byte[] DeriveKey(byte[] password, byte[] salt, int rounds)
        {
            var generator = new Pkcs5S2ParametersGenerator(new Sha256Digest());
            generator.Init(password, salt, rounds);
            var key = ((KeyParameter)generator.GenerateDerivedMacParameters(256)).GetKey();
            password = null;
            return key;
        }
    }
}
