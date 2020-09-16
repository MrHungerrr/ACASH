using System.Xml.Linq;
using Vkimow.Serializators.XML;


namespace Overwatch.Memorable
{
    public interface IMemorableInfo : IXMLSerializable
    {
        int Id { get; }
        MemorableManager.types type { get; }
    }
}