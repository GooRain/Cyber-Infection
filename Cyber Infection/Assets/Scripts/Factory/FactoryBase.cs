using UnityEngine;

namespace Factory
{
	public abstract class FactoryBase<TProduct> : MonoBehaviour
	{
		protected abstract TProduct CreateProduct();

		public TProduct GetProduct()
		{
			return CreateProduct();
		}
	}
}