//-----------------------------------------------------------------------
// <copyright file="DesEncryptorFull.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
// 
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Security.Cryptography;
using System.Text;
using Genesys.Extensions;
using Genesys.Extras.Mathematics;
using Genesys.Extras.Text.Encoding;

namespace Genesys.Extras.Security.Cryptography
{
    /// <summary>
    /// Encrypts/Decrypts using 3 DES algorithms
    /// </summary>
    [CLSCompliant(true)]
    public class DesEncryptor : IEncryptor
    {
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; private set; } = "193FFC71-1DD6-4AAD-B75C-936A002940B3";
        /// <summary>
        /// Salt
        /// </summary>
        public string Salt { get; private set; } = Arithmetic.Random().ToString();
        /// <summary>
        /// EncodeForURL
        /// </summary>
        public bool EncodeForURL { get; protected set; } = TypeExtension.DefaultBoolean;
        
        /// <summary>
        /// Force immutability
        /// </summary>
        private DesEncryptor()
            : base()
        {
            EncodeForURL = true;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="encryptionKey"></param>
        /// <param name="urlEncode"></param>
        public DesEncryptor(string encryptionKey = "", bool urlEncode = true)
            : this()
        {            
            Key = encryptionKey;
            EncodeForURL = urlEncode;
        }
        
        /// <summary>
        /// Encrypts a string
        /// </summary>
        public string Encrypt(string originalString)
        {
            var returnValue = TypeExtension.DefaultString;

            try
            {
                var saltedString = originalString + this.Salt;
                TripleDES des = CreateDes();
                ICryptoTransform encryptor = des.CreateEncryptor();
                var encryptedByte = Encoding.Unicode.GetBytes(saltedString);
                // Final encryption and return
                returnValue = Convert.ToBase64String(encryptor.TransformFinalBlock(encryptedByte, 0, encryptedByte.Length));
                if (this.EncodeForURL)
                {
                    returnValue = UrlEncoder.Encode(returnValue);
                }
            }
            catch
            {
                returnValue = TypeExtension.DefaultString;
            }

            return returnValue;
        }
        
        /// <summary>
        /// Decrypts an salted string
        /// </summary>
        /// <param name="encryptedString"></param>
        public string Decrypt(string encryptedString)
        {
            var returnValue = TypeExtension.DefaultString;
            var itemToDecrypt = TypeExtension.DefaultString;

            try
            {
                itemToDecrypt = encryptedString;
                if (this.EncodeForURL)
                {
                    itemToDecrypt = UrlEncoder.Decode(encryptedString);
                }
                TripleDES des = CreateDes();
                ICryptoTransform decryptor = des.CreateDecryptor();
                var encryptedByte = Convert.FromBase64String(itemToDecrypt);
                var decryptedByte = decryptor.TransformFinalBlock(encryptedByte, 0, encryptedByte.Length);
                var decryptedSaltedString = Encoding.Unicode.GetString(decryptedByte);
                // Final decryption and return
                returnValue = decryptedSaltedString.Remove(decryptedSaltedString.Length - this.Salt.Length);
            }
            catch
            {
                returnValue = TypeExtension.DefaultString;
            }

            return returnValue;
        }
        
        /// <summary>
        /// Takes a string as key value, calculates MD5 hash on input parameter string.  
        /// This hash value would be used as real key for the encryption. 
        /// </summary>
        private TripleDES CreateDes()
        {
            MD5 md5Provider = new MD5CryptoServiceProvider();
            TripleDES returnValue = new TripleDESCryptoServiceProvider();
            returnValue.Key = md5Provider.ComputeHash(Encoding.Unicode.GetBytes(this.Key));
            returnValue.IV = new byte[Convert.ToInt32(returnValue.BlockSize / 8 - 1) + 1];

            return returnValue;
        }
    }
}
