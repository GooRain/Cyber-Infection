﻿using Data.Settings.Base;
using Extension;
using Generation.Tiles;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace Data.Settings.Generation
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
		
		public Point roomsRange;
		public RoomSizeInfo roomSizeInfo;
		public Rectangle mapSize;

		[Space(10), Header("Visual Settings")] 
		[SerializeField]
		private Tile _floorTile;
		[SerializeField]
		private Tile _wallTile;
		[SerializeField]
		private Gradient _colorGradient;
		
		public Tile GetFloorTile()
		{
			return _floorTile;
		}

		public Tile GetWallTile()
		{
			return _wallTile;
		}

		public Color GetColor(float value = .5f)
		{
			return _colorGradient.Evaluate(value);
		}
	}

	[System.Serializable]
	public struct RoomSizeInfo
	{
		[FormerlySerializedAs("minRoomWidth")] public int roomWidth;
		[FormerlySerializedAs("minRoomHeight")] public int roomHeight;
	}
}