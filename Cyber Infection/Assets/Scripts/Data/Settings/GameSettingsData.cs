using System.Collections.Generic;
using Data.Loading;
using Data.Settings.Base;
using Data.Settings.Generation;
using UnityEngine;

namespace Data.Settings
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

		[SerializeField] private InputSettingsData _inputSettingsData;

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

		public InputSettingsData inputSettingsData
		{
			get
			{
				if (_inputSettingsData == null)
				{
					_inputSettingsData = CreateInstance<InputSettingsData>();
					Debug.Log("_inputSettingsData == null");
				}

				return _inputSettingsData;
			}
			set { _inputSettingsData = value; }
		}

		#endregion
	}
}