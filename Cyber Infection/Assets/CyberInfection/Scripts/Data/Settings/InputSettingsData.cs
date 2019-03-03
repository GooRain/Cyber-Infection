using CyberInfection.Data.Settings.Base;
using CyberInfection.Extension.Utility;
using UnityEngine;

namespace CyberInfection.Data.Settings
{
	[CreateAssetMenu(menuName = "Cyber Infection/Data/Input Settings", order = 2)]
	public class InputSettingsData : SettingsDataBase<InputSettingsData>
	{
		private const string AssetPath = "Data/Settings/InputSettingsData";
		public override InputSettingsData GetCopy()
		{
			return Instantiate(TryToLoad(AssetPath));
		}
		
		[System.Serializable]
		public class KeyCodesDictionary : SerializableDictionary<string, KeyCode>
		{
		}

		[SerializeField] private KeyCodesDictionary _keyCodesDictionary;

		public KeyCode GetKeyCode(string key)
		{
			return _keyCodesDictionary.ContainsKey(key) ? _keyCodesDictionary[key] : KeyCode.JoystickButton19;
		}
	}
}