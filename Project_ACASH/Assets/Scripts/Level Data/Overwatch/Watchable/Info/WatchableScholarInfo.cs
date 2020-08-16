using UnityEngine;
using System.Xml.Linq;

namespace Overwatch.Watchable
{
    public struct WatchableScholarInfo : IWatchableInfo
    {
        public int Id { get; private set; }
        public WatchableManager.types type => WatchableManager.types.Scholar;


        public Vector3 BodyPosition { get; private set; }
        public float BodyRotation { get; private set; }
        public Vector3 HeadPosition { get; private set; }
        public Quaternion HeadRotation { get; private set; }

        public int AnimationId { get; private set; }
        public float AnimationTime { get; private set; }



        public WatchableScholarInfo(in int id)
        {
            Id = id;

            BodyPosition = Vector3.zero;
            BodyRotation = 0;
            HeadPosition = Vector3.zero;
            HeadRotation = Quaternion.identity;

            AnimationId = -1;
            AnimationTime = 0;
        }


        public void Capture(in Vector3 bodyPosition, in float bodyRotation, in Vector3 headPosition, in Quaternion headRotation, in int animationId, in float animationTime)
        {
            BodyPosition = bodyPosition;
            BodyRotation = bodyRotation;
            HeadPosition = headPosition;
            HeadRotation = headRotation;

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

            bufferElement = xElement.Element("Head").Element("Rotation");

            HeadRotation = new Quaternion(
                (float)bufferElement.Element("X"),
                (float)bufferElement.Element("Y"),
                (float)bufferElement.Element("Z"),
                (float)bufferElement.Element("W")
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
                            ),
                        new XElement("Rotation",
                            new XElement("X", HeadRotation.x),
                            new XElement("Y", HeadRotation.y),
                            new XElement("Z", HeadRotation.z),
                            new XElement("W", HeadRotation.w)
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
