using Persistent.Settings;
using UnityEngine;

namespace Persistent
{
	public class Singleton<T> : MonoBehaviour
	{
		public static T instance { get; protected set; }
	}
}