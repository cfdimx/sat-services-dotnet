using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SAT.Core.Helpers
{
    public static class XmlHelper
    {
        public static XmlDocument ToXmlDocument(this string xml)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                return doc;
            }
            catch
            {
                return null;
            }
        }
        public static string SerializeDocumentToXml<T>(T obj)
        {
            try
            {
                string xmlString = null;
                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer xs = new XmlSerializer(typeof(T));
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                xs.Serialize(xmlTextWriter, obj);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                xmlString = Encoding.UTF8.GetString(memoryStream.ToArray());
                return RemoveInvalidCharsXml(xmlString);
            }
            catch
            {
                return string.Empty;
            }
        }
        public static T DeserealizeDocument<T>(string xml)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                stream = new StringReader(xml);
                reader = new XmlTextReader(stream);
                return (T)serializer.Deserialize(reader);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (stream != null) stream.Close();
                if (reader != null) reader.Close();
            }
        }
        public static string RemoveInvalidCharsXml(string str)
        {
            str = str.Replace("\r\n", "");
            str = str.Replace("\r", "");
            str = str.Replace("\n", "");
            str = str.Replace(@"<?xml version=""1.0"" encoding=""utf-16""?>", @"<?xml version=""1.0"" encoding=""utf-8""?>").Trim();
            str = str.Replace("﻿", "");
            str = str.Replace(@"
", "");
            return str;
        }
    }
}
