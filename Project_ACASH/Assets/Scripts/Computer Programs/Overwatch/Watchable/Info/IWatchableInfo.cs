using System.Xml.Linq;
using XMLSerialize;


namespace Overwatch.Watchable
{
    public interface IWatchableInfo : IXMLSerializable
    {
        int Id { get; }
        WatchableManager.types type { get; }
    }
}