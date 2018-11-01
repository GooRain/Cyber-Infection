using UnityEngine;

namespace Data
{
	public class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
	{
		public static T instance;

		protected T GetInstance()
		{
			return this as T;
		}

		protected virtual void OnEnable()
		{
			instance = GetInstance();
			DebugInstance();
		}

		private void DebugInstance()
		{
			//Debug.Log("Path updated for " + name + " = <color=red>" + instance + "</color>");
		}
	}
}