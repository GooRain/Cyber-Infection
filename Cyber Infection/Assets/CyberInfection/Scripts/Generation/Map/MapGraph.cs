using CyberInfection.Extension;

namespace CyberInfection.Generation.Map
{
    public class MapGraph : Graph
    {
        public MapGraph(int roomsCount) : base(roomsCount)
        {
            Generate();
        }
    }
}
