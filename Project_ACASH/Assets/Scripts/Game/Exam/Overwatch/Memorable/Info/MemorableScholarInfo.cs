using UnityEngine;
using System.Xml.Linq;

namespace Overwatch.Memorable
{
    public struct MemorableScholarInfo : IMemorableInfo
    {
        public int Id { get; private set; }
        public MemorableManager.types type => MemorableManager.types.Scholar;


        public Vector2 Position { get; private set; }
        public float Rotation { get; private set; }



        public MemorableScholarInfo(int id)
        {
            Id = id;

            Position = Vector2.zero;
            Rotation = 0;
        }


        public void Capture(in Vector2 position, float rotation)
        {
            Position = position;
            Rotation = rotation;
        }


        public void ReadXML(XElement xElement)
        {
            Id = (int)xElement.Attribute("Id");

            #region Position & Rotation
            var bufferElement = xElement.Element("Position");

            Position = new Vector2(
                (float)bufferElement.Element("X"),
                (float)bufferElement.Element("Y")
                );

            Rotation = (float) xElement.Element("Rotation");
            #endregion
        }

        public XElement ConvertToXML()
        {
            return ConvertToXML("Scholar");
        }

        public XElement ConvertToXML(string name)
        {
            XElement xElement = new XElement(name, 
                
                new XAttribute("Id", Id), new XAttribute("Type", type),

                    #region Position & Rotation

                    new XElement("Position",
                        new XElement("X", Position.x),
                        new XElement("Y", Position.y)
                    ),

                    new XElement("Rotation", Rotation)
                     #endregion
                );

            return xElement;
        }
    }
}
