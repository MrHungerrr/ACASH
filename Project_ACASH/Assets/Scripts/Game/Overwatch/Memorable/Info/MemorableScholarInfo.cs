using UnityEngine;
using System.Xml.Linq;

namespace Overwatch.Memorable
{
    public struct MemorableScholarInfo : IMemorableInfo
    {
        public int Id { get; private set; }
        public MemorableManager.types type => MemorableManager.types.Scholar;


        public Vector3 BodyPosition { get; private set; }
        public float BodyRotation { get; private set; }
        public Vector3 HeadPosition { get; private set; }

        public int AnimationId { get; private set; }
        public float AnimationTime { get; private set; }



        public MemorableScholarInfo(int id)
        {
            Id = id;

            BodyPosition = Vector3.zero;
            BodyRotation = 0;
            HeadPosition = Vector3.zero;

            AnimationId = -1;
            AnimationTime = 0;
        }


        public void Capture(in Vector3 bodyPosition, float bodyRotation, Vector3 headPosition, int animationId, float animationTime)
        {
            BodyPosition = bodyPosition;
            BodyRotation = bodyRotation;
            HeadPosition = headPosition;

            AnimationId = animationId;
            AnimationTime = animationTime;
        }


        public void ReadXML(XElement xElement)
        {
            Id = (int)xElement.Attribute("Id");

            #region Body
            var bufferElement = xElement.Element("Body").Element("Position");

            BodyPosition = new Vector3(
                (float)bufferElement.Element("X"),
                (float)bufferElement.Element("Y"),
                (float)bufferElement.Element("Z")
                );

            BodyRotation = (float) xElement.Element("Body").Element("Rotation");
            #endregion

            #region Head
            bufferElement = xElement.Element("Head").Element("Position");

            HeadPosition = new Vector3(
                (float)bufferElement.Element("X"),
                (float)bufferElement.Element("Y"),
                (float)bufferElement.Element("Z")
                );
            #endregion

            #region Animation
            bufferElement = xElement.Element("Animation");

            AnimationId = (int) bufferElement.Element("Id");
            AnimationTime = (float) bufferElement.Element("Time");
            #endregion

        }

        public XElement ConvertToXML()
        {
            XElement xElement = new XElement
                (
                    "Scholar", new XAttribute("Id", Id), new XAttribute("Type", type),

                    new XElement("Body",
                        new XElement("Position",
                            new XElement("X", BodyPosition.x),
                            new XElement("Y", BodyPosition.y),
                            new XElement("Z", BodyPosition.z)
                            ),
                        new XElement("Rotation", BodyRotation)
                    ),

                    new XElement("Head",
                        new XElement("Position",
                            new XElement("X", HeadPosition.x),
                            new XElement("Y", HeadPosition.y),
                            new XElement("Z", HeadPosition.z)
                            )
                    ),

                    new XElement("Animation",
                        new XElement("Id", AnimationId),
                        new XElement("Time", AnimationTime)
                    )
                );

            return xElement;
        }
    }
}
