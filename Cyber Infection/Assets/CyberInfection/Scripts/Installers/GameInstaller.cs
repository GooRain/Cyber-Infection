using CyberInfection.Data.Settings.Generation;
using CyberInfection.GameMechanics.Unit.Player;
using CyberInfection.Persistent.Settings;
using Plugins.Zenject.Source.Install;
using UnityEngine;

namespace CyberInfection.Installers
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<GameSettings>().AsSingle();
            Container.Bind<GeneratingScenesData>().FromResources(GeneratingScenesData.AssetPath).AsSingle();
        }
    }
}