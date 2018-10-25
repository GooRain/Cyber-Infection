using UnityEngine;

namespace Data.Settings
{
	public abstract class SettingsDataBase<T> : ScriptableSingleton<T> where T : ScriptableObject
	{
	}
}