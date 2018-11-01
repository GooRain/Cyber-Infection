using UnityEngine;

namespace Data.Settings
{
	public abstract class SettingsDataBase<T> : SingletonScriptableObject<T> where T : ScriptableObject
	{
	}
}