namespace GameMechanic.Weapon
{
	public interface IWeapon
	{
		float recoil { get; set; }
		
		void TryShoot();
		void Shoot();
		void TryReload();
		void Reload();
	}
}