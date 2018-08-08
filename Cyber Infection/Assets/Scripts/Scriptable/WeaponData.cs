using UnityEngine;

namespace Scriptable
{
	[CreateAssetMenu(fileName = "Weapon Data", menuName = "Cyber Infection/Create Weapon Data", order = 0)]
	public class WeaponData : ScriptableObject
	{
		public float recoil = 0f;
	}
}