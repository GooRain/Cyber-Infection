using UnityEngine;

namespace Data.Settings
{
	[CreateAssetMenu(menuName = "Cyber Infection/Data/Input Settings", order = 2)]
	public class InputSettingsData : SettingsDataBase<InputSettingsData>
	{
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