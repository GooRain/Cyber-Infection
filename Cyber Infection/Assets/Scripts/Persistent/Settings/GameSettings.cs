using Data.Settings;
using UnityEngine;
using UnityEngine.Serialization;

namespace Persistent.Settings
{
	public class GameSettings : SettingsBase
	{
		[Header("Game Data")]
		[SerializeField] private GameSettingsData _data;

		[Space(10)]
		[Header("Runtime Game Data")] 
		[SerializeField, FormerlySerializedAs("Do Copy Of Every Data")]
		private bool _doCopyOfData;
		[SerializeField] private GameSettingsData _loadedData;
		
		public GameSettingsData data => _loadedData;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void InitializeGameSettings()
		{
			
		}

		private void InitializeLoadData()
		{
			if (_doCopyOfData)
			{
				_loadedData = _data.GetCopy();
				_loadedData.generatingScenesData = _data.generatingScenesData.GetCopy();
				_loadedData.inputSettingsData = _data.inputSettingsData.GetCopy();
				_loadedData.mapSettingsData = _data.mapSettingsData.GetCopy();
			}
			else
			{
				_loadedData = _data;
			}
			
		}
	}
}