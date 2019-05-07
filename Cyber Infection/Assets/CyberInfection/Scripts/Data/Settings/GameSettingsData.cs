using CyberInfection.Data.Settings.Base;
using CyberInfection.Data.Settings.Generation;
using UnityEngine;

namespace CyberInfection.Data.Settings
{
	[CreateAssetMenu(menuName = "Cyber Infection/Data/Game Settings", order = 0)]
	public class GameSettingsData : SettingsDataBase<GameSettingsData>
	{
		private const string AssetPath = "Data/Base/GameSettingsData";

		public override GameSettingsData GetCopy()
		{
			return Instantiate(TryToLoad(AssetPath));
		}
		
		#region SerializedFields
		
		[SerializeField] private MapSettingsData _mapSettingsData;

		[SerializeField] private GeneratingScenesData _generatingScenesData;

		#endregion


		#region Properties

		public GeneratingScenesData generatingScenesData
		{
			get
			{
				if (_generatingScenesData == null)
				{
					_generatingScenesData = CreateInstance<GeneratingScenesData>();
					Debug.Log("_generatingScenesData == null");
				}

				return _generatingScenesData;
			}
			set { _generatingScenesData = value; }
		}

		public MapSettingsData mapSettingsData
		{
			get
			{
				if (_mapSettingsData == null)
				{
					_mapSettingsData = CreateInstance<MapSettingsData>();
					Debug.Log("_mapSettingsData == null");
				}

				return _mapSettingsData;
			}
			set { _mapSettingsData = value; }
		}

		#endregion
	}
}