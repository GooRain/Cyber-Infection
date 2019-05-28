using CyberInfection.Generation.Room;
using UnityEngine;

namespace CyberInfection.Data.Settings.Generation
{
    [CreateAssetMenu(menuName = "Cyber Infection/Data/Color Tile Type Data")]
    public class ColorTileTypeData : ScriptableObject
    {
        [SerializeField] private ColorTileTypeDictionary colorTileTypeDictionary;
        
        public ColorTileTypeDictionary ColorTileTypeDictionary => colorTileTypeDictionary;
    }
}