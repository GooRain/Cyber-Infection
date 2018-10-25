using UnityEngine;

namespace Data.Settings
{
	[CreateAssetMenu(menuName = "Cyber Infection/Data/Map Settings", order = 1)]
	public class MapSettingsData : SettingsDataBase<MapSettingsData>
	{
		public int minAreaSize = 6;
		public int maxAreaSize = 20;
		public Rectangle mapSize;
	}
}