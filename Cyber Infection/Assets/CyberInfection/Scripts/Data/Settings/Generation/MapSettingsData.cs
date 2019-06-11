using CyberInfection.Data.Settings.Base;
using CyberInfection.Extension;
using CyberInfection.Generation.Tiles;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace CyberInfection.Data.Settings.Generation
{
	[CreateAssetMenu(menuName = "Cyber Infection/Data/Map Settings", order = 1)]
	public class MapSettingsData : SettingsDataBase<MapSettingsData>
	{
		private const string AssetPath = "Data/Settings/MapSettingsData";
		public override MapSettingsData GetCopy()
		{
			return Instantiate(TryToLoad(AssetPath));
		}

		//public string seed;
		
		[Space(10)]
		
		public Point roomsRange;
		public RoomSizeInfo roomSizeInfo;
		public Rectangle mapSize;

		[Space(10)] 
		
		public AnimationCurve difficultyCurve;

		[Space(10), Header("Visual Settings")] 
		
		[SerializeField] private TileBase floorTile;
		[SerializeField] private TileBase wallTile;
		[SerializeField] private TileBase doorTile;
		[SerializeField] private TileBase shadowTile;
        
		[SerializeField] private TileTypeTileDictionary tileTypeTile;
        
		public TileTypeTileDictionary TileTypeTileDictionary => tileTypeTile;

		public TileBase GetTile(TileType type)
		{
			return tileTypeTile[type];
		}
		
		public TileBase GetFloorTile()
		{
			return floorTile;
		}

		public TileBase GetWallTile()
		{
			return wallTile;
		}

		public TileBase GetDoorTile()
		{
			return doorTile;
		}

		public TileBase GetShadowTile()
		{
			return shadowTile;
		}
	}

	[System.Serializable]
	public struct RoomSizeInfo
	{
		[FormerlySerializedAs("minRoomWidth")] public int roomWidth;
		[FormerlySerializedAs("minRoomHeight")] public int roomHeight;
	}

	[System.Serializable]
	public class TileTypeTileDictionary : SerializableDictionaryBase<TileType, TileBase>
	{
        
	}
}