﻿//-----------------------------------------------------------------------
// <copyright file="PHPSErializerGeneric.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
// 
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Genesys.Extensions;

namespace Genesys.Extras.Serialization
{
    /// <summary>
    /// Serializer Class for PHP
    ///types:
    /// N = null
    /// s = string
    /// i = int
    /// d = double
    /// a = array
    /// </summary>
    [CLSCompliant(true)]
    public class PhpSerializer<ObjectType> : Serializer<ObjectType> where ObjectType : new()
    {
        private Dictionary<Hashtable, bool> seenHashtables;
        private Dictionary<ArrayList, bool> seenArrayLists;
        private System.Globalization.NumberFormatInfo nfi;
        private int pos;

        /// <summary>
        /// for deserialize
        /// </summary>
        public bool XMLSafe = true;

        /// <summary>
        /// StringEncoding
        /// </summary>
        public Encoding StringEncoding = new System.Text.UTF8Encoding();        
        
        /// <summary>
        /// Constructor
        /// </summary>
        public PhpSerializer() : base()
        {
            nfi = new System.Globalization.NumberFormatInfo();
            nfi.NumberGroupSeparator = TypeExtension.DefaultString;
            nfi.NumberDecimalSeparator = ".";
        }
        
        /// <summary>
        /// Serializes and returns the object as a string
        /// </summary>
        /// <param name="objectToSerialize"></param>
        /// <returns>Serialized string</returns>
        public override string Serialize(ObjectType objectToSerialize)
        {
            seenArrayLists = new Dictionary<ArrayList, bool>();
            seenHashtables = new Dictionary<Hashtable, bool>();

            return SerializeWorker(objectToSerialize, new StringBuilder()).ToString();
        }

        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <param name="sb">string builder to append</param>
        /// <returns></returns>
        private StringBuilder SerializeWorker(object obj, StringBuilder sb)
        {
            if (obj == null)
            {
                return sb.Append("N;");
            }
            else if (obj is string)
            {
                string str = (string)obj;
                if (this.XMLSafe)
                {
                    str = str.Replace("\r" + "\n", "\n");
                    str = str.Replace("\r", "\n");
                }
                return sb.Append("s:" + this.StringEncoding.GetByteCount(str) + ":\"" + str + "\";");
            }
            else if (obj is bool)
            {
                return sb.Append("b:" + (Convert.ToBoolean(obj) ? "1" : "0") + ";");
            }
            else if (obj is int)
            {
                int i = Convert.ToInt32(obj);
                return sb.Append("i:" + i.ToString(this.nfi) + ";");
            }
            else if (obj is double)
            {
                double d = Convert.ToDouble(obj);

                return sb.Append("d:" + d.ToString(this.nfi) + ";");
            }
            else if (obj is ArrayList)
            {
                if (this.seenArrayLists.ContainsKey((ArrayList)obj))
                {
                    return sb.Append("N;");
                }
                else
                {
                    //cycle detected
                    seenArrayLists.Add((ArrayList)obj, true);
                }

                ArrayList a = (ArrayList)obj;
                sb.Append("a:" + a.Count + ":{");
                for (var i = 0; i <= a.Count - 1; i++)
                {
                    SerializeWorker(i, sb);
                    SerializeWorker(a[i], sb);
                }
                sb.Append("}");
                return sb;
            }
            else if (obj is Hashtable)
            {
                if (this.seenHashtables.ContainsKey((Hashtable)obj))
                {
                    return sb.Append("N;");
                }
                else
                {
                    //cycle detected
                    seenHashtables.Add((Hashtable)obj, true);
                }

                Hashtable a = (Hashtable)obj;
                sb.Append("a:" + a.Count + ":{");
                foreach (DictionaryEntry entry in a)
                {
                    SerializeWorker(entry.Key, sb);
                    SerializeWorker(entry.Value, sb);
                }
                sb.Append("}");
                return sb;
            }
            else
            {
                return sb;
            }
        }
        
        /// <summary>
        /// Deserialize from string
        /// </summary>
        /// <param name="StringToDeserialize"></param>
        /// <returns>Deserialized string</returns>
        public override ObjectType Deserialize(string StringToDeserialize)
        {
            pos = 0;
            return (ObjectType)DeserializeWorker(StringToDeserialize);
        }

        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="str">string to deserialize</param>
        /// <returns>De-serialized string</returns>
        private object DeserializeWorker(string str)
        {
            if (str == null || str.Length <= this.pos)
            {
                return new object();
            }

            int start = 0;
            int end = 0;
            int length = 0;
            string stLen = null;
            switch (str.SubstringSafe(this.pos, 1))
            {
                case "N":
                    pos += 2;
                    return null;
                case "b":
                    char chBool = '\0';
                    chBool = str.SubstringSafe(this.pos, 2).ToCharArray(0, 1)[0];
                    pos += 4;
                    return chBool == '1';
                case "i":
                    string stInt = null;
                    start = str.IndexOf(":", this.pos) + 1;
                    end = str.IndexOf(";", start);
                    stInt = str.Substring(start, end - start);
                    pos += 3 + stInt.Length;
                    return int.Parse(stInt, this.nfi);
                case "d":
                    string stDouble = null;
                    start = str.IndexOf(":", this.pos) + 1;
                    end = str.IndexOf(";", start);
                    stDouble = str.Substring(start, end - start);
                    pos += 3 + stDouble.Length;
                    return Double.Parse(stDouble, this.nfi);
                case "s":
                    start = str.IndexOf(":", this.pos) + 1;
                    end = str.IndexOf(":", start);
                    stLen = str.Substring(start, end - start);
                    int bytelen = int.Parse(stLen);
                    length = bytelen;
                    //This is the byte length, not the character length - so we migth  
                    //need to shorten it before usage. This also implies bounds checking
                    if ((end + 2 + length) >= str.Length)
                    {
                        length = str.Length - 2 - end;
                    }
                    string stRet = str.Substring(end + 2, length);
                    while (this.StringEncoding.GetByteCount(stRet) > bytelen)
                    {
                        length -= 1;
                        stRet = str.Substring(end + 2, length);
                    }
                    pos += 6 + stLen.Length + length;
                    if (this.XMLSafe)
                    {
                        stRet = stRet.Replace("\n", "\r" + "\n");
                    }
                    return stRet;
                case "a":
                    //if keys are ints 0 through N, returns an ArrayList, else returns Hashtable
                    start = str.IndexOf(":", this.pos) + 1;
                    end = str.IndexOf(":", start);
                    stLen = str.Substring(start, end - start);
                    length = int.Parse(stLen);
                    Hashtable htRet = new Hashtable(length);
                    ArrayList alRet = new ArrayList(length);
                    pos += 4 + stLen.Length;
                    //a:Len:{
                    for (var i = 0; i <= length - 1; i++)
                    {
                        //read key
                        object key = DeserializeWorker(str);
                        //read value
                        object val = DeserializeWorker(str);

                        if (alRet != null)
                        {
                            if (key is int && Convert.ToInt32(key) == alRet.Count)
                            {
                                alRet.Add(val);
                            }
                            else
                            {
                                alRet = null;
                            }
                        }
                        htRet[key] = val;
                    }

                    pos += 1;
                    //skip the }
                    if (this.pos < str.Length && str.SubstringSafe(this.pos, 1) == ";")
                    {
                        //skipping our old extra array semi-colon bug (er... php's weirdness)
                        pos += 1;
                    }
                    if (alRet != null)
                    {
                        return alRet;
                    }
                    else
                    {
                        return htRet;
                    }
                default:
                    return TypeExtension.DefaultString;
            }
        }
    }
}
