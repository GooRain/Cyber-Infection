using Data.Settings;
using UnityEngine;

namespace Persistent.Settings
{
	public class GameSettings : SettingsBase<GameSettings>
	{
		[SerializeField] private GameSettingsData _data;

		private void Awake()
		{
			_data = (GameSettingsData) GameSettingsData.instance;
		}
	}
}