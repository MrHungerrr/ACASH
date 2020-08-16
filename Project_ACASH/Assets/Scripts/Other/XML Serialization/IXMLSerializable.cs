using System.Xml.Linq;

namespace XMLSerialize
{
    public interface IXMLSerializable
    {
        XElement ConvertToXML();

        void ReadXML(XElement xElement);
    }
}

