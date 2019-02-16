using CyberInfection.GameMechanics.Bullet;
using UnityEngine;

namespace CyberInfection.Data.Object
{
	[CreateAssetMenu( menuName = "Cyber Infection/Weapon/WeaponData")]
	public class WeaponData : ScriptableObject
	{
		[Header("Parameters")] 
		
		public int maxAmmoInMagazine;
		public int startAmmunitionSize;// mb
		
		public float shootRate;
		
		[Header("Prefabs")]
		
		public Bullet bulletPrefab;
	}
}