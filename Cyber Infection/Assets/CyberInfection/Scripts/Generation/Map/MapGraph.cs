using CyberInfection.Extension;

namespace CyberInfection.Generation
{
    public class MapGraph : Graph
    {
        public MapGraph(int roomsCount) : base(roomsCount)
        {
            Generate();
        }
    }
}
