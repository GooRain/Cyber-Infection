using Data.Settings.Base;
using Extension;
using UnityEngine;

namespace Data.Settings.Generation
{
	[CreateAssetMenu(menuName = "Cyber Infection/Data/Map Settings", order = 1)]
	public class MapSettingsData : SettingsDataBase<MapSettingsData>
	{
		private const string AssetPath = "Data/Settings/MapSettingsData";
		public override MapSettingsData LoadAsset()
		{
			return Instantiate(TryToLoad(AssetPath));
		}
		
		public Point roomsRange;
		public RoomSizeInfo roomSizeInfo;
		public Rectangle mapSize;

		[Space(10), Header("Visual Settings")] [SerializeField]
		private Material _floorMaterial;
		[SerializeField]
		private Gradient _colorGradient;
		
		public Material GetFloorMaterial()
		{
			return _floorMaterial;
		}

		public Color GetColor(float value = .5f)
		{
			return _colorGradient.Evaluate(value);
		}
	}

	[System.Serializable]
	public struct RoomSizeInfo
	{
		public float minRoomWidth;
		public float minRoomHeight;
		public float maxRoomWidth;
		public float maxRoomHeight;
	}
}