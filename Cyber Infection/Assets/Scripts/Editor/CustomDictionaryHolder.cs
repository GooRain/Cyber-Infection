using Data;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomPropertyDrawer(typeof(InputSettings.KeyCodesDictionary))]
	public class KeyCodesDictionaryDrawer : DictionaryDrawer<string, KeyCode>
	{
	}
}