namespace CyberInfection.GameMechanics
{
	public interface IAlive : IDamagable
	{
		int health { get; set; }
        void RestoreHealth(int healthAmount);
		void Die();
	}
}