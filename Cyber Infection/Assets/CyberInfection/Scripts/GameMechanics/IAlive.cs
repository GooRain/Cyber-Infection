namespace CyberInfection.GameMechanics
{
	public interface IAlive
	{
		int health { get; set; }
		void GetDamage(int damageAmount);
		void Die();
	}
}