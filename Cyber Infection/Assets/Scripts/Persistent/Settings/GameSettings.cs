using Data.Settings;
using UnityEngine;

namespace Persistent.Settings
{
	public class GameSettings : SettingsBase<GameSettings>
	{
		[SerializeField] private GameSettingsData _data;
		
		public GameSettingsData data { get; private set; }

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void InitializeGameSettings()
		{
			InstantiateFromPrefab("Prefabs/InstantiateBeforeScene/GameSettings");
			_instance.InitializeLoadData();
		}
		
		private void InitializeLoadData()
		{
			data = _data.LoadAsset();
			//data.generatingScenesData = null;
			//data.generatingScenesData = _data.generatingScenesData.LoadAsset();
			//data.inputSettingsData = _data.inputSettingsData.LoadAsset();
			//data.mapSettingsData = _data.mapSettingsData.LoadAsset();
		}
	}
}