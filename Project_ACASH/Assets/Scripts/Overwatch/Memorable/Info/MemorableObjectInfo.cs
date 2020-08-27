using UnityEngine;
using System.Xml.Linq;

namespace Overwatch.Memorable
{

    public struct MemorableObjectInfo : IMemorableInfo
    {
        public int Id { get; private set; }
        public MemorableManager.types type => MemorableManager.types.Object;


        public Vector3 Position { get; private set; }
        public Quaternion Rotation { get; private set; }


        public MemorableObjectInfo(in int id)
        {
            Id = id;
            Position = Vector3.zero;
            Rotation = Quaternion.identity;
        }


        public void Capture(in Vector3 position, in Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }


        public void ReadXML(XElement xElement)
        {
            Id = (int)xElement.Attribute("Id");

            var bufferElement = xElement.Element("Position");

            Position = new Vector3(
                (float)bufferElement.Element("X"),
                (float)bufferElement.Element("Y"),
                (float)bufferElement.Element("Z")
                );

            bufferElement = xElement.Element("Rotation");

            Rotation = new Quaternion(
                (float)bufferElement.Element("X"),
                (float)bufferElement.Element("Y"),
                (float)bufferElement.Element("Z"),
                (float)bufferElement.Element("W")
                );
        }

        public XElement ConvertToXML()
        {
            XElement xElement = new XElement
                (
                    "Object", new XAttribute("Id", Id), new XAttribute("Type", type),
                    new XElement("Position",
                        new XElement("X", Position.x),
                        new XElement("Y", Position.y),
                        new XElement("Z", Position.z)
                    ),
                    new XElement("Rotation",
                        new XElement("X", Rotation.x),
                        new XElement("Y", Rotation.y),
                        new XElement("Z", Rotation.z),
                        new XElement("W", Rotation.w)
                    )
                );

            return xElement;
        }
    }
}
