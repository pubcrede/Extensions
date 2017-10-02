//-----------------------------------------------------------------------
// <copyright file="MD5HashBuilderFull.cs" company="Genesys Source">
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

namespace Genesys.Extras.Security.Cryptography
{
    /// <summary>
    /// Builds a hashed string from raw text
    /// </summary>
    [CLSCompliant(true)]
    public class Md5HashBuilder
    {
        private string salt = "0873F24C-FA01-4811-AB36-2F079D4CA0D9";
        /// <summary>
        /// HashedString
        /// </summary>
        public string HashedString { get; protected set; } = TypeExtension.DefaultString;

        /// <summary>
        /// Force immutability
        /// </summary>
        private Md5HashBuilder() : base() {}

        /// <summary>
        /// Force immutability
        /// </summary>
        private Md5HashBuilder(string salt) : this() { if (salt != TypeExtension.DefaultString) this.salt = salt; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stringToHash">string to hash</param>
        /// <param name="salt">Salted value to add to hashed string</param>
        public Md5HashBuilder(string stringToHash, string salt = "")
            : this(salt)
        {
            HashedString = this.HashCreate(stringToHash);
        }

        /// <summary>
        /// Hashes a String
        /// </summary>
        /// <param name="stringToHash">string to hash</param>
        /// <returns>Hashed string data</returns>
        private string HashCreate(string stringToHash)
        {
            var returnValue = TypeExtension.DefaultString;
            var hashValue = new StringBuilder();

            try
            {
                stringToHash += this.salt;
                using (MD5 MD55Hash = MD5.Create())
                {
                var Data = MD55Hash.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));
                    for (var Count = 0; Count <= Data.Length - 1; Count++)
                    {
                        hashValue.Append(Data[Count].ToString("x2"));
                    }
                }
                returnValue = hashValue.ToString();
            }
            catch
            {
                returnValue = TypeExtension.DefaultString;
            }

            return returnValue;
        }
        
        /// <summary>
        /// Hashes and compares a string
        /// </summary>
        /// <param name="rawString">string to hash compare</param>
        /// <returns>True if string + salt hashed matches current hash string</returns>
        public bool Compare(string rawString)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            var returnValue = TypeExtension.DefaultBoolean;
            var rawStringHash = TypeExtension.DefaultString;

            rawStringHash = this.HashCreate(rawString);
            if (0 == comparer.Compare(rawStringHash, this.HashedString))
            {
                returnValue = true;
            }

            return returnValue;
        }
    }
}
