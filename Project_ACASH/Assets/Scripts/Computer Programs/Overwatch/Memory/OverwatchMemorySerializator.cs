using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Overwatch.Watchable;

namespace Overwatch.Memory
{
    public static class OverwatchMemorySerializator
    {
        public static OverwatchMemoryCell ReadCell(XElement xElement)
        {
            int size = (int) xElement.Attribute("Size");
            var moments = new OverwatchMemoryMoment[size];

            var elements = from element in xElement.Elements()
                           orderby (int) element.Attribute("Index")
                           select element;

            int index = 0;

            foreach (var element in elements)
            {
                moments[index] = ReadMoment(element);
                index++;
            }

            var cell = new OverwatchMemoryCell(moments);
            return cell;
        }


        private static OverwatchMemoryMoment ReadMoment(XElement xElement)
        {
            var moment = new OverwatchMemoryMoment((float)xElement.Attribute("Time"));

            var bufferElements = xElement.Element("Info").Elements();

            if (bufferElements != null)
            {
                IWatchableInfo info;

                foreach (var element in bufferElements)
                {
                    info = ReadInfo(element);
                    moment.Add(info);
                }
            }

            return moment;
        }


        private static IWatchableInfo ReadInfo(XElement xElement)
        {
            switch (xElement.Attribute("Type").Value)
            {
                case "Scholar":
                    {
                        var scholarInfo = new WatchableScholarInfo();
                        scholarInfo.ReadXML(xElement);
                        return scholarInfo;
                    }
                case "Object":
                    {
                        var objectInfo = new WatchableObjectInfo();
                        objectInfo.ReadXML(xElement);
                        return objectInfo;
                    }
                default:
                    {
                        throw new Exception($"Неправильный тип объекта - { xElement.Attribute("Type").Value }");
                    }
            }
        }



        public static XElement ToXML(this OverwatchMemoryCell memoryCell)
        {
            var element = new XElement("Overwatch_Memory_Cell",
                new XAttribute("Size", memoryCell.Size)
                );

            for (int i = 0; i < memoryCell.Size; i++)
            {
                element.Add(memoryCell[i].ToXML(i));
            }

            return element;
        }


        private static XElement ToXML(this in OverwatchMemoryMoment memoryMoment, int index)
        {
            var element = new XElement("Moment", 
                new XAttribute("Index", index),
                new XAttribute("Time", memoryMoment.Time),
                new XElement("Info")
                );

            var bufferElement = element.Element("Info");

            for (int i = 0; i < memoryMoment.Info.Count; i++)
            {
                bufferElement.Add(memoryMoment.Info[i].ConvertToXML());
            }

            return element;
        }
    }
}
