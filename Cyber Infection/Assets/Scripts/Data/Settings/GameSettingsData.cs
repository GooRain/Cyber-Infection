using UnityEditor;
using UnityEngine;

namespace Data.Settings
{
	[CreateAssetMenu(menuName = "Cyber Infection/Data/Game Settings", order = 0)]
	public class GameSettingsData : SettingsDataBase
	{
		public static GameSettingsData instance;
		
		private void OnEnable()
		{
			instance = this;
			Debug.Log("Path updated for " + name + " = <color=red>" + instance + "</color>");
		}
		
		public string roomName;
	}
}