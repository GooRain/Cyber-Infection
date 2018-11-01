using UnityEngine;

namespace Persistent
{
	/// <inheritdoc />
	/// Inherit from Monobehaviour class
	/// <summary>
	/// Be aware this will not prevent a non singleton constructor
	///   such as `T myT = new T();`
	/// To prevent that, add `protected T () {}` to your singleton class.
	/// As a note, this is made as MonoBehaviour because we need Coroutines.
	/// </summary>
	public class SingletonMonobehaviour<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance;

		public static T instance
		{
			get
			{
				if (_applicationIsQuitting)
				{
					Debug.LogWarning("[SingletonMonobehaviour] instance '" + typeof(T) +
					                 "' already destroyed on application quit." +
					                 " Won't create again - returning null.");
					return null;
				}
				
				if (_instance == null)
				{
					InstantiateSingleton();
				}
				
				return _instance;
			}
		}

		protected static void InstantiateSingleton()
		{
			var singleton = new GameObject();
			_instance = singleton.AddComponent<T>();
			singleton.name = "(singleton) " + typeof(T);

			DontDestroyOnLoad(singleton);
		}
		
		// ReSharper disable once StaticMemberInGenericType
		private static bool _applicationIsQuitting;

		/// <summary>
		/// When Unity quits, it destroys objects in a random order.
		/// In principle, a SingletonMonobehaviour is only destroyed when application quits.
		/// If any script calls instance after it have been destroyed, 
		///   it will create a buggy ghost object that will stay on the Editor scene
		///   even after stopping playing the Application. Really bad!
		/// So, this was made to be sure we're not creating that buggy ghost object.
		/// </summary>
		public void OnDestroy()
		{
			_applicationIsQuitting = true;
		}
	}
}