using System.Xml.Linq;
using XMLSerialize;


namespace Overwatch.Memorable
{
    public interface IMemorableInfo : IXMLSerializable
    {
        int Id { get; }
        MemorableManager.types type { get; }
    }
}