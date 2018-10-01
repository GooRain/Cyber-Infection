using System.Collections.Generic;
using UnityEngine;

namespace Data
{
	[CreateAssetMenu(fileName = "Input Settings", menuName = "Cyber Infection/Create new Input Settings", order = 0)]
	public class InputSettings : ScriptableObject
	{
		[System.Serializable]
		public class KeyCodesDictionary : SerializableDictionary<string, KeyCode>
		{
		}

		public KeyCodesDictionary keyCodes = new KeyCodesDictionary();

		public KeyCode this[string key]
		{
			get { return keyCodes[key]; }
			set { keyCodes[key] = value; }
		}
	}
}