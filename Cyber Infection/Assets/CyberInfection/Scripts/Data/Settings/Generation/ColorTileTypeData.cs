using CyberInfection.Generation.Room;
using CyberInfection.Generation.Tiles;
using UnityEngine;

namespace CyberInfection.Data.Settings.Generation
{
    [CreateAssetMenu(menuName = "Cyber Infection/Data/Color Tile Type Data")]
    public class ColorTileTypeData : ScriptableObject
    {
        [SerializeField] private ColorTileTypeDictionary colorTileTypeDictionary;
        
        public TileType GetTileType(Color color)
        {
            return colorTileTypeDictionary.ContainsKey(color) ? colorTileTypeDictionary[color] : TileType.Floor;
        }
    }
}