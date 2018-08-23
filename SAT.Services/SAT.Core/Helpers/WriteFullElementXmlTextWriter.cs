using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SAT.Core.Helpers
{
    public class MessageSerializer : XmlTextWriter
    {
        public MessageSerializer(Stream stream) : base(stream, Encoding.UTF8)
        {
        }

        public MessageSerializer(string filename, Encoding encoding) : base(filename, encoding)
        {
        }

        public MessageSerializer(Stream w, Encoding encoding) : base(w, encoding)
        {
        }

        public override void WriteEndElement()
        {
            base.WriteFullEndElement();
        }
    }
}
