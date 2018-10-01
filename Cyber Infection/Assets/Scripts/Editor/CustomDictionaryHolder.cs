using Data.Settings;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomPropertyDrawer(typeof(InputSettingsData.KeyCodesDictionary))]
	public class KeyCodesDictionaryDrawer : DictionaryDrawer<string, KeyCode>
	{
	}
}