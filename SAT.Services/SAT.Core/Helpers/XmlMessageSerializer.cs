using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SAT.Core.Helpers
{
    public static class XmlMessageSerializer
    {
        public static string SerializeDocumentToXml<T>(T obj)
        {
            try
            {
                string xmlString = null;
                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer xs = new XmlSerializer(typeof(T));
                MessageSerializer xmlTextWriter = new MessageSerializer(memoryStream, Encoding.UTF8);
                xs.Serialize(xmlTextWriter, obj);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                xmlString = Encoding.UTF8.GetString(memoryStream.ToArray());
                return Helpers.XmlHelper.RemoveInvalidCharsXml(xmlString);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
