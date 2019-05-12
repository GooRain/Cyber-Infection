using CyberInfection.GameMechanics.Entity.Units;

namespace CyberInfection.Factory.EnemyFactory
{
	public class EnemyFactoryBase : FactoryBase<Enemy> {
		
		protected override Enemy CreateProduct()
		{
			return gameObject.AddComponent<Enemy>();
		}
	}
}
