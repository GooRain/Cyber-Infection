using Data.Settings.Generation;
using GameMechanic.Unit.Player;
using Persistent.Settings;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        [SerializeField] private Player _player;
        
        public override void InstallBindings()
        {
            Container.Bind<GameSettings>().AsSingle();
            Container.Bind<GeneratingScenesData>().FromResources(GeneratingScenesData.AssetPath).AsSingle();

            Container.Bind<Player>().FromInstance(_player);
        }
    }
}