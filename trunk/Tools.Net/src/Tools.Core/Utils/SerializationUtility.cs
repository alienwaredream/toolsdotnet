using System;
using System.IO;
using System.Text;
//using Tools.Core.document;
using System.Xml;
using System.Xml.Serialization;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Tools.Core.Utils
{
	/// <summary>
	/// Summary description for SerializationUtil.
	/// </summary>
	public class SerializationUtility
	{

		/// <summary>
		/// Serialize object to byte array(binary serialization is used)
		/// </summary>
		/// <param name="source">object to be serialized</param>
        /// <exception cref="ArgumentNullException">When source is null this exception is raised</exception>
		/// <exception cref="SerializationUtilException">
		/// thrown when serialization fails
		/// </exception>
		/// <returns>serialization result as byte array</returns>
		public static byte[] Serialize2ByteArray(object source)
		{
			byte[] retVal = null;
			
			using (MemoryStream ms = new MemoryStream())
			{
				BinaryFormatter formatter = new BinaryFormatter();
				//formatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
                    
					formatter.Serialize(ms, source);
					
					// converting serialization result to a byte array
					retVal = ms.ToArray();
			}

			return retVal;

		}

		/// <summary>
		/// Deserialize object from byte array(binary serialization is used)
		/// </summary>
		/// <param name="objectByteArrayGraph">byte array to deserialize from</param>
		/// <exception cref="SerializationUtilException">
		/// thrown when serialization fails
		/// </exception>
		/// <returns>deserialized object</returns>
		public static object DeserializeFromByteArray(byte[] objectByteArrayGraph)
		{
			object retVal = null;

			if (objectByteArrayGraph != null)
			{
				MemoryStream ms = null;
				BinaryFormatter formatter = new BinaryFormatter();
				try
				{
					// creating MemoryStream from byte array
					ms = new MemoryStream(objectByteArrayGraph, false);
					
					// deserializing
					retVal = formatter.Deserialize(ms);
					// Complete post deserialization work.
					IDeserializationCallback idc = retVal as IDeserializationCallback;
					if (idc!=null)
					{
						idc.OnDeserialization(null);
					}
				}
				finally
				{
					if (ms != null)
					{
						ms.Close();
					}
				}
			}
			
			return retVal;

		}


        /// <summary>
        /// Gets the object clone binary.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns></returns>
		public static object GetObjectCloneBinary(object original)
		{
			return 
				DeserializeFromByteArray
				(
				Serialize2ByteArray
				(
				original
				));
		}

        /// <summary>
        /// Serializes the object to file.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="append">if set to <c>true</c> [append].</param>
        /// <param name="writeAsFragment">if set to <c>true</c> [write as fragment].</param>
        public static void Serialize2File(object source, string fileName, bool append, bool writeAsFragment)
        {
            System.Xml.Serialization.XmlSerializer xs = null;
            System.IO.TextWriter tw = null;

            //Monitor.Enter(syncFileWriteLock);

            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(fileName))) Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                xs = new System.Xml.Serialization.XmlSerializer(source.GetType());

                tw = new StreamWriter(fileName, append);


                if (writeAsFragment)
                {
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.OmitXmlDeclaration = true;
                    settings.NewLineOnAttributes = true;

                    using (System.Xml.XmlWriter xmlWriter =
                       XmlWriter.Create(tw, settings))
                    {

                        xs.Serialize(xmlWriter, source);
                        xmlWriter.Flush();
                    }
                }
                else
                {
                    xs.Serialize(tw, source);
                }

            }
            catch (Exception ex)
            {
                throw new
                    Exception
                    (
                    "Error while writing to file, SerializationUtil.Serialize2File, " +
                    ex.ToString(),
                    ex);
            }
            finally
            {
                if (tw != null) tw.Close();
                //Monitor.Exit(syncFileWriteLock);
            }
        }
        /// <summary>
        /// Serializes the object to string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static string Serialize2String(object source)
        {
            //IDefaultNamespaceHolder nh = source as IDefaultNamespaceHolder;
            return Serialize2String(source, null /*((nh!=null) ? nh.Namespace : null)*/);
        }

        /// <summary>
        /// Serializes the object to string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="defaultns">The defaultns.</param>
        /// <returns></returns>
        public static string Serialize2String(object source, string defaultns)
        {
            if (source == null) return null;

            string retVal = String.Empty;
            XmlSerializer xs = null;
            StringBuilder sb = null;
            System.IO.StringWriter sw = null;
            try
            {
                XmlQualifiedName defns = new XmlQualifiedName("", defaultns);
                XmlQualifiedName xsins =
                    new XmlQualifiedName
                    (
                    "xsi",
                    "http://www.w3.org/2001/XMLSchema-instance"
                    );
                XmlSerializerNamespaces nss =
                    new XmlSerializerNamespaces
                    (
                    new XmlQualifiedName[] { defns, xsins }
                    );
                //XmlSerializerNamespaces nss = new XmlSerializerNamespaces(new XmlQualifiedName[] {defns});
                xs = new XmlSerializer(source.GetType(), defaultns);
                sb = new StringBuilder();
                sw = new System.IO.StringWriter(sb);

                xs.Serialize(sw, source, nss);
                retVal = sb.ToString();
            }
            catch (Exception e)
            {
                throw new Exception("Serialize2String Failed " + e.ToString());
            }
            finally
            {
                sw.Close();
            }
            return retVal;

        }

        public static object DeserializeFromString(string objectTextGraph, System.Type type)
        {
            object ctc = null;
            if (objectTextGraph != null || objectTextGraph != String.Empty)
            {
                System.Xml.XmlTextReader reader = null;
                try
                {
                    System.Xml.Serialization.XmlSerializer xs = new XmlSerializer(type);
                    //sr = new StringReader(objectTextGraph);
                    reader = new XmlTextReader(objectTextGraph, XmlNodeType.Element, null);
                    reader.Normalization = false;
                    ctc = xs.Deserialize(reader);
                    // Complete post deserialization work.
                    IDeserializationCallback idc = ctc as IDeserializationCallback;
                    if (idc != null)
                    {
                        idc.OnDeserialization(null);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception
                        (
                        "Exception ocurred while reading type " + type.FullName + " from the objectTextGraph " + objectTextGraph + "\r\n" + e.ToString() + "\r\nType:" + e.GetType().Name,
                        e
                        );
                }
                finally
                {
                    if (reader != null) reader.Close();
                }
            }
            return ctc;
        }

	}
}
