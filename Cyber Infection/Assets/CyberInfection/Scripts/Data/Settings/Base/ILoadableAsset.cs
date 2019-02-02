namespace CyberInfection.Data.Settings.Base
{
	public interface ILoadableAsset<out T>
	{
		T GetCopy();
	}
}