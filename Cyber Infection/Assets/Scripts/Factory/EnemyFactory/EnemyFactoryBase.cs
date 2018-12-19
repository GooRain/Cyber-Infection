using GameMechanic.Unit.Enemy;

namespace Factory.EnemyFactory
{
	public class EnemyFactoryBase : FactoryBase<Enemy> {
		
		protected override Enemy CreateProduct()
		{
			return gameObject.AddComponent<Enemy>();
		}
	}
}
