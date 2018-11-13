using Data.Settings.Generation;
using Persistent.Settings;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameSettings>().AsSingle();
            Container.Bind<GeneratingScenesData>().FromResources(GeneratingScenesData.AssetPath).AsSingle();
        }
    }
}