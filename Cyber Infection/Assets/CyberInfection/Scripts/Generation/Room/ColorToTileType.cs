using CyberInfection.Generation.Tiles;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace CyberInfection.Generation.Room
{
    [System.Serializable]
    public class ColorToTileType 
    {
        [SerializeField] private Color color;
        [SerializeField] private TileType type;

        public Color Color => color;
        public TileType Type => type;
    }

    [System.Serializable]
    public class ColorTileTypeDictionary : SerializableDictionaryBase<Color, TileType>
    {
        
    }
}