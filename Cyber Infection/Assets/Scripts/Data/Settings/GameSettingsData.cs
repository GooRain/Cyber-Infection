using UnityEditor;
using UnityEngine;

namespace Data.Settings
{
	[CreateAssetMenu(menuName = "Cyber Infection/Data/Game Settings", order = 0)]
	public class GameSettingsData : SettingsDataBase<GameSettingsData>
	{
		public string roomName;
	}
}