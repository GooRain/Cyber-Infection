using UnityEngine;

namespace CyberInfection.Data.Entities
{
	[CreateAssetMenu( menuName = "Cyber Infection/Weapon/Weapon Data")]
	public class WeaponData : ScriptableObject
	{
		[Header("Parameters")] 
		
		public int maxAmmoInMagazine;
		public int startAmmunitionSize;// mb
		
		public float shootRate;
		
		[Header("Prefabs")]
		
		public BulletData bulletData;
	}
}