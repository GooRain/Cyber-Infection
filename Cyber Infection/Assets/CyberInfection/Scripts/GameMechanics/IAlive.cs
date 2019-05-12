namespace CyberInfection.GameMechanics
{
	public interface IAlive
	{
		int health { get; set; }
        void RestoreHealth(int healthAmount);
		void GetDamage(int damageAmount);
		void Die();
	}
}