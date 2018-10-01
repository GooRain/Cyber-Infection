using UnityEngine;

namespace Data.Settings
{
	[CreateAssetMenu(menuName = "Cyber Infection/Data/Input Settings", order = 2)]
	public class InputSettingsData : SettingsDataBase
	{
		[System.Serializable]
		public class KeyCodesDictionary : SerializableDictionary<string, KeyCode>
		{
			
		}
		
		public static InputSettingsData instance;
		
		private void OnEnable()
		{
			instance = this;
			Debug.Log("Path updated for " + name + " = <color=red>" + instance + "</color>");
		}
		
		[SerializeField]
		private KeyCodesDictionary _keyCodesDictionary;

		public KeyCode GetKeyCode(string key)
		{
			if (_keyCodesDictionary.ContainsKey(key))
			{
				return _keyCodesDictionary[key];
			}
			return KeyCode.JoystickButton19;
		}
	}
}