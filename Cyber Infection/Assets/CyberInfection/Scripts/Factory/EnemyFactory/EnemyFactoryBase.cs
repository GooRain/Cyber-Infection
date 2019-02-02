using CyberInfection.GameMechanics.Unit.Enemy;

namespace CyberInfection.Factory.EnemyFactory
{
	public class EnemyFactoryBase : FactoryBase<Enemy> {
		
		protected override Enemy CreateProduct()
		{
			return gameObject.AddComponent<Enemy>();
		}
	}
}
