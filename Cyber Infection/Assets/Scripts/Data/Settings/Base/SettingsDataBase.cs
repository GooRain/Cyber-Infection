using UnityEngine;

namespace Data.Settings.Base
{
	public abstract class SettingsDataBase<T> : ScriptableObject, ILoadableAsset<T> where T : SettingsDataBase<T>
	{
		public abstract T GetCopy();

		protected static T TryToLoad(string path)
		{
			var asset = Resources.Load<T>(path);

			if (asset == null)
			{
				Debug.LogError("Create asset of type " + typeof(T) + " in path: " + path);
			}

			return asset;
		}
	}
}