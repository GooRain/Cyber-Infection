using CyberInfection.Data.Settings.Base;
using CyberInfection.Extension;
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

		public string seed;
		
		[Space(10)]
		
		public Point roomsRange;
		public RoomSizeInfo roomSizeInfo;
		public Rectangle mapSize;

		[Space(10), Header("Visual Settings")] 
		
		[SerializeField] private TileBase _floorTile;
		[SerializeField] private TileBase _wallTile;
		[SerializeField] private TileBase _shadowTile;
		
		public TileBase GetFloorTile()
		{
			return _floorTile;
		}

		public TileBase GetWallTile()
		{
			return _wallTile;
		}

		public TileBase GetShadowTile()
		{
			return _shadowTile;
		}
	}

	[System.Serializable]
	public struct RoomSizeInfo
	{
		[FormerlySerializedAs("minRoomWidth")] public int roomWidth;
		[FormerlySerializedAs("minRoomHeight")] public int roomHeight;
	}
}