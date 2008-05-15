using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;
using System.IO;
using System.Diagnostics.CodeAnalysis;
using Tools.Common.Asserts;

namespace Tools.Common.Utils
{
    /// <summary>
    /// Helper class for testing classes to be serializable as data contracts.
    /// For more flexible serialization use <see cref="SerializationUtility"></see> instead
    /// </summary>
    public class DataContractSerializationUtility
    {
        /// <summary>
        /// Serializes the object to string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static string Serialize2String<T>(T source)
        {
            StringBuilder sb = new StringBuilder();

            using (XmlWriter xw = XmlWriter.Create(sb))
            {
                using (XmlDictionaryWriter xdw = XmlDictionaryWriter.CreateDictionaryWriter(xw))
                {
                    DataContractSerializer dcs = new DataContractSerializer(typeof(T));
                    dcs.WriteObject(xdw, source);
                }
            }
            return sb.ToString();
        }
		public static string Serialize2String(object source)
		{
			ErrorTrap.AddRaisableAssertion<ArgumentNullException>(source != null, "source != null");

			StringBuilder sb = new StringBuilder();

			using (XmlWriter xw = XmlWriter.Create(sb))
			{
				using (XmlDictionaryWriter xdw = XmlDictionaryWriter.CreateDictionaryWriter(xw))
				{
					DataContractSerializer dcs = new DataContractSerializer(source.GetType());
					dcs.WriteObject(xdw, source);
				}
			}
			return sb.ToString();
		}
        /// <summary>
        /// Deserializes from string.
        /// </summary>
        /// <typeparam name="T">The type of the return object.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification="T is type of a return value. Parameter of type T can't be provided.")]
        public static T DeserializeFromString<T>(string source)
        {
            using (StringReader sr = new StringReader(source))
            {
                using (XmlReader xr = XmlReader.Create(sr))
                {
                    using (XmlDictionaryReader xdr = XmlDictionaryReader.CreateDictionaryReader(xr))
                    {
                        DataContractSerializer dcs = new DataContractSerializer(typeof(T));
                        return (T)dcs.ReadObject(xdr, true);
                    }
                }
            }
            
        }
		/// <summary>
		/// Deserializes from string.
		/// </summary>
		/// <typeparam name="T">The type of the return object.</typeparam>
		/// <param name="source">The source.</param>
		/// <returns></returns>
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "T is type of a return value. Parameter of type T can't be provided.")]
		public static object DeserializeFromString(string source, Type type)
		{
			using (StringReader sr = new StringReader(source))
			{
				using (XmlReader xr = XmlReader.Create(sr))
				{
					using (XmlDictionaryReader xdr = XmlDictionaryReader.CreateDictionaryReader(xr))
					{
						DataContractSerializer dcs = new DataContractSerializer(type);
						return dcs.ReadObject(xdr, true);
					}
				}
			}

		}
        /// <summary>
        /// Deserializes from byte array.
        /// </summary>
        /// <typeparam name="T">The type of the return object</typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "T is type of a return value. Parameter of type T can't be provided.")]
        public static T DeserializeFromByteArray<T>(byte[] source)
        {
            throw new NotImplementedException("TODO: Implement this method correctly.");
            //using (XmlDictionaryReader xdr = XmlDictionaryReader.CreateBinaryReader(source, XmlDictionaryReaderQuotas.Max))
            //{
            //    DataContractSerializer dcs = new DataContractSerializer(typeof(T));
            //    return (T)dcs.ReadObject(xdr, true);
            //}
        }
        /// <summary>
        /// Serializes the source object to byte array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static byte[] Serialize2ByteArray<T>(T source)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (XmlDictionaryWriter xdw = XmlDictionaryWriter.CreateBinaryWriter(ms))
                {
                    DataContractSerializer dcs = new DataContractSerializer(typeof(T));
                    dcs.WriteObject(ms, source);
                    return ms.GetBuffer();
                }
                
            }
        }

        /// <summary>
        /// Clones the via text.
        /// </summary>
        /// <typeparam name="T">The type of the object to clone.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static T CloneViaText<T>(T source)
        {
            return DeserializeFromString<T>(Serialize2String<T>(source));
        }
        /// <summary>
        /// Clones the via text.
        /// </summary>
        /// <typeparam name="T">The type of the object to clone.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static object CloneViaText(object source)
        {
			ErrorTrap.AddRaisableAssertion<ArgumentNullException>(source != null, "source != null");

            return DeserializeFromString(Serialize2String(source), source.GetType());
        }
        #region Commented out
        //private static XmlDictionaryWriter ResolveXmlDictionaryWriter(
        //    DataContractSerializationOptions options, XmlWriter xw)
        //{
        //    if (options == DataContractSerializationOptions.None || options == DataContractSerializationOptions.Dictionary)
        //    {
        //        return XmlDictionaryWriter.CreateDictionaryWriter(xw);
        //    }
        //    if (options == DataContractSerializationOptions.Binary)
        //    {
        //        return XmlDictionaryWriter.CreateBinaryWriter(xw);
        //    }
        //} 
        #endregion

    }
}
