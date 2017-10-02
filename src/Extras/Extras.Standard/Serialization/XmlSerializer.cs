//-----------------------------------------------------------------------
// <copyright file="XmlSerializerFull.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
// 
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.IO;
using System.Xml;
using Genesys.Extensions;
using Genesys.Extras.Collections;


namespace Genesys.Extras.Serialization
{
    /// <summary>
    /// XML serialization and deserialization
    /// </summary>
    [CLSCompliant(true)]
    public class XmlSerializer<T> : Serializer<T> where T : new()
    {
        /// <summary>
        /// List of types that allow serializer to use a type not explicitly defined. 
        ///   Primarily used to define ISerialier Of IMyType, but pass in object of MyType 
        ///   (serializer blows up on now knowing that MyType exists, only knows about IMyType)
        /// </summary>
        public new IListSafe<Type> KnownTypes { get; set; } = new ListSafe<Type>();

        /// <summary>
        /// Constructor
        /// </summary>
        public XmlSerializer()
            : base()
        {
        }

        /// <summary>
        /// Serializes and returns the JSON as a string
        /// </summary>
        /// <param name="objectToSerialize">Item to serialize</param>
        /// <returns>string serialized with passed object</returns>
        public override string Serialize(T objectToSerialize)
        {
            var returnValue = TypeExtension.DefaultString;
            var stream = new MemoryStream();

            try
            {
                if (objectToSerialize == null && this.EmptyStringAndNullSupported == false) { throw new System.ArgumentNullException("Passed parameter is null. Unable to serialize null objects."); }
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(objectToSerialize.GetType());
                var textWriter = new XmlTextWriter(stream, System.Text.Encoding.UTF8);
                System.Text.UTF8Encoding Encoding = new System.Text.UTF8Encoding();
                xs.Serialize(stream, objectToSerialize);
                stream = (MemoryStream)textWriter.BaseStream;
                returnValue = Encoding.GetString(stream.ToArray());
            }
            catch
            {
                if (ThrowException) throw;
            }

            return returnValue;
        }
        
        /// <summary>
        /// De-serializes the passed string to an object
        /// </summary>
        /// <param name="stringToDeserialize">Object to deserialize</param>
        /// <returns>Concrete class</returns>
        public override T Deserialize(string stringToDeserialize)
        {
            T returnValue = TypeExtension.InvokeConstructorOrDefault<T>();

            try
            {
                if (stringToDeserialize == TypeExtension.DefaultString && this.EmptyStringAndNullSupported == false) { throw new System.ArgumentNullException("Passed parameter is empty. Unable to deserialize empty strings."); }
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                var byteArray = (byte[])encoding.GetBytes(stringToDeserialize);
                var memoryStream = new MemoryStream(byteArray);
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
                returnValue = (T)xs.Deserialize(memoryStream);
            }
            catch (System.InvalidOperationException)
            {
                returnValue = (T)Activator.CreateInstance(typeof(T));
            }
            catch
            {
                if (ThrowException) throw;
            }

            return returnValue;
        }
    }
}
