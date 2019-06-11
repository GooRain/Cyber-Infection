using CyberInfection.Data.Settings.Generation;
using CyberInfection.Generation.Tiles;
using UnityEngine;

namespace CyberInfection.Generation.Room
{
    public class RoomTemplate
    {
        public readonly int width;
        public readonly int height;
        public readonly TileType[,] tiles;

        public TileType this[int x, int y] => tiles[x, y];

        public RoomTemplate(Texture2D texture2D, ColorTileTypeData colorTileTypeData)
        {
            var pixels = texture2D.GetPixels();
            
            width = texture2D.width;
            height = texture2D.height;
            tiles = new TileType[width, height];

            for(var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    tiles[x, y] = colorTileTypeData.GetTileType(pixels[x + width * y]);
                }
            }
        }
    }
}