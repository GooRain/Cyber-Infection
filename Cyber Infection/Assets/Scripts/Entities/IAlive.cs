namespace Entities
{
	public interface IAlive
	{
		int health { get; set; }
		void GetDamage(int damageAmount);
		void Die();
	}
}