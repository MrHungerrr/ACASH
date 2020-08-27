using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Overwatch.Memory;
using System.Xml.Linq;
using Overwatch;
using System.IO;
using Single;

public static class OverwatchDataManager
{
    private static readonly string PATH = Application.dataPath + @"\OverwatchData";


    public static void Setup()
    {
        DirectoryManager.Create(PATH);
    }


    public static void SetLevel()
    {
        DirectoryManager.Clear(PATH);
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
        return $"{PATH}\\{index}.xml";
    }

}
