using UnityEngine;

namespace CyberInfection.GameMechanics.Weapon.WeaponTypes
{
	public interface IWeapon
	{
		float recoil { get; set; }
		
		void Shoot(Vector2 direction);
		void Reload();
	}
}