using CyberInfection.Data.Settings;
using UnityEngine;

namespace CyberInfection.Persistent.Settings
{
	public class GameSettings : SettingsBase
	{
		[Header("Game Data")]
		
		[SerializeField] private GameSettingsData m_Data;

		[Space(10)]
		[Header("Runtime Game Data")] 
		
		[SerializeField] private bool m_DoCopyOfData;
		[SerializeField] private GameSettingsData m_LoadedData;
		
		public GameSettingsData data => m_LoadedData;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void InitializeGameSettings()
		{
			
		}

		private void InitializeLoadData()
		{
			if (m_DoCopyOfData)
			{
				m_LoadedData = m_Data.GetCopy();
				m_LoadedData.generatingScenesData = m_Data.generatingScenesData.GetCopy();
				m_LoadedData.mapSettingsData = m_Data.mapSettingsData.GetCopy();
			}
			else
			{
				m_LoadedData = m_Data;
			}
			
		}
	}
}