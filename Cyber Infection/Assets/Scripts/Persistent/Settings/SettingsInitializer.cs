using UnityEngine;

namespace Persistent.Settings
{
	public class SettingsInitializer : SettingsBase<SettingsInitializer>
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void InitializeInstances()
		{
			Debug.Log("Initializing instances");
			GameSettings.instance = new GameObject("Game Settings").AddComponent<GameSettings>();
			DontDestroyOnLoad(GameSettings.instance.gameObject);
			MapSettings.instance = new GameObject("Map Settings").AddComponent<MapSettings>();
			DontDestroyOnLoad(MapSettings.instance.gameObject);
		}
	}
}