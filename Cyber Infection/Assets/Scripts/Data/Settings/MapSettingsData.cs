using UnityEngine;

namespace Data.Settings
{
	[CreateAssetMenu(menuName = "Cyber Infection/Data/Map Settings", order = 1)]
	public class MapSettingsData : SettingsDataBase
	{
		public static MapSettingsData instance;
		
		private void OnEnable()
		{
			instance = this;
			Debug.Log("Path updated for " + name + " = <color=red>" + instance + "</color>");
		}
		
		public int minAreaSize = 6;
		public int maxAreaSize = 20;
		public Rectangle mapSize;
	}
}