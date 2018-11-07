namespace Data.Settings.Base
{
	public interface ILoadableAsset<out T>
	{
		T LoadAsset();
	}
}