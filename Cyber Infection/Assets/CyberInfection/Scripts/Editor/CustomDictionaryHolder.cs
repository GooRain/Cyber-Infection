using CyberInfection.Data.Settings;
using UnityEditor;
using UnityEngine;

namespace CyberInfection.Editor
{
	[CustomPropertyDrawer(typeof(InputSettingsData.KeyCodesDictionary))]
	public class KeyCodesDictionaryDrawer : DictionaryDrawer<string, KeyCode>
	{
	}
}