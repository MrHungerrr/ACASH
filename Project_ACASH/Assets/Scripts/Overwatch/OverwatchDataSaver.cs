using Overwatch.Memory;
using System.Xml.Linq;
using System;
using MultiTasking;
using UnityEngine;
using System.IO;
using System.Xml;
using Single;
using System.Threading.Tasks;

namespace Overwatch
{
    public static class OverwatchDataSaver
    {
        private static string _path;

        public static void Setup()
        {
            _path = LevelDataManager.Path + @"\OverwatchHistory";
            DirectoryManager.Create(_path);
        }


        public static void SetLevel()
        {
            DirectoryManager.Clear(_path);
        }


        public static void Save(in OverwatchMemoryCell cell, in int index)
        {
            var path = Path(index);
            var xElement = cell.ToXML();
            var xDocument = new XDocument(new XDeclaration("1,0", "utf-8", "yes"), xElement);
            xDocument.Save(path);
        }


        public static OverwatchMemoryCell Load(in int index)
        {
            var path = Path(index);
            var xDocument = XDocument.Load(path);
            var cell = OverwatchMemorySerializator.ReadCell(xDocument.Root);
            return cell;
        }


        public static string Path(in int index)
        {
            return $"{_path}\\{index}.xml";
        }
    }
}
