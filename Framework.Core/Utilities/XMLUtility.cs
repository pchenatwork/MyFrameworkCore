using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Framework.Core.Utilities
{
    public static class XMLUtility
    {
        public static string ToXml(object obj)
        {
            var a = obj.GetType();
            //var x = typeof(a);
            return ToXml(obj, obj.GetType().Name);
        }
        public static string ToXml(object obj, string xmlRootName)
        {
            TextReader reader = null;
            try
            {
                return Serialize(obj, xmlRootName);
                //   return reader.ReadToEnd();
            }

            catch (System.Exception e)
            {
                //if (_logger.IsErrorEnabled)
                //{
                //    _logger.Error(e.Message);
                //}
                return null;
            }
            finally
            {
                reader.Close();
            }
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceXML"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string sourceXML) where T : class
        {
            /**** Borrow the idea from here, good read : 
             * https://stackoverflow.com/questions/2347642/deserialize-from-string-instead-textreader/42796061
             * ************************************************/
            var serializer = new XmlSerializer(typeof(T));
            T result = null;

            ///**using (TextReader reader = new StringReader(sourceXML))
            ////using (XmlTextReader reader = new XmlTextReader(sourceXML))
            using (StringReader reader = new StringReader(sourceXML))
            {
                result = (T)serializer.Deserialize(reader);
            }

            return result;
        }
        public static T Deserialize<T>(XmlReader reader)
        {
            object result = null;
            XmlSerializer mySerializer;
            try
            {
                mySerializer = new XmlSerializer(typeof(T));
                result = mySerializer.Deserialize(reader);
            }
            catch (System.Exception e)
            {

                var a = e.Message;
                var b = reader.Name;
                //  if (_logger.IsErrorEnabled)
                //  {
                //      _logger.Error(e.Message + reader.Name);
                //  }
            }
            finally
            {
                mySerializer = null;
            }
            return (T)result;
        }


        public static string Serialize<T>(this T toSerialize, string xmlRootName = null)
        {
            return Serialize<T>(toSerialize, xmlRootName, null);
        }

        public static string Serialize<T>(this T toSerialize, string xmlRootName = null, string nameSpace = null)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), CreateXmlRootAttribute(xmlRootName, nameSpace));
            using (StringWriter writer = new StringWriter())
            {
                xmlSerializer.Serialize(writer, toSerialize);
                var x = writer.ToString();
                //writer.Close();
                return x;
            }

            /**
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.NamespaceHandling
            StringWriter sw = new StringWriter();

            using (XmlWriter writer = XmlWriter.Create(sw, settings))
    ****/
        }



        #region	Private Methods

        private static XmlRootAttribute CreateXmlRootAttribute(string xmlRootName, string nameSpace)
        {
            var x = new XmlRootAttribute();
            XmlRootAttribute xmlRoot = new XmlRootAttribute
            {
                ElementName = string.IsNullOrWhiteSpace(xmlRootName) ? string.Empty : xmlRootName,
                Namespace = string.IsNullOrWhiteSpace(nameSpace) ? string.Empty : nameSpace, // string.Empty;
                IsNullable = true
            };
            return xmlRoot;
        }

        #endregion	Private Methods

    }
}
