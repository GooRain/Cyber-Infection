using UnityEngine;

namespace CyberInfection.Data
{
	public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
	{
		protected const string AssetPath = "";

		private static T _instance;
		protected static T instance
		{
			get
			{
				if (_instance) return _instance;
				
				if (!InitializeLoadAsset())
				{
					Debug.LogWarning("<color=red>ScriptableObject</color>: There is no Asset of type " + typeof(T) +
					                 " in path: " + "<color=green>" + AssetPath + "</color>");
				}

				return _instance;
			}
		}
		
		protected static bool InitializeLoadAsset()
		{
			_instance = Resources.Load<T>(AssetPath);

			return _instance != null;
		}
	}
}