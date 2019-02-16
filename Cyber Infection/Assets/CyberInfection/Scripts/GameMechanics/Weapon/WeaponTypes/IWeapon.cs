namespace CyberInfection.GameMechanics.Weapon.WeaponTypes
{
	public interface IWeapon
	{
		float recoil { get; set; }
		
		void TryToShoot();
		void TryToReload();
	}
}