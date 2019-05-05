using CyberInfection.GameMechanics.Entity.Enemy;

namespace CyberInfection.Factory.EnemyFactory
{
	public class EnemyFactoryBase : FactoryBase<Enemy> {
		
		protected override Enemy CreateProduct()
		{
			return gameObject.AddComponent<Enemy>();
		}
	}
}
