using Plugins.Zenject.OptionalExtras.Scripts.Enemy;
using Plugins.Zenject.OptionalExtras.Signals.Internal.Binders;
using Plugins.Zenject.OptionalExtras.Signals.Main;
using Plugins.Zenject.Source.Install;
using UnityEngine;

namespace Plugins.Zenject.OptionalExtras.Scripts.Installers
{
    // Include this just to ensure BindSignal with an object mapping works
    public class PlayerDiedSignalObserver
    {
        public void OnPlayerDied()
        {
            Debug.Log("Fired PlayerDiedSignal");
        }
    }

    public class GameSignalsInstaller : Installer<GameSignalsInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<EnemyKilledSignal>();
            Container.DeclareSignal<PlayerDiedSignal>();

            // Include these just to ensure BindSignal works
            Container.BindSignal<PlayerDiedSignal>().ToMethod<PlayerDiedSignalObserver>(x => x.OnPlayerDied).FromNew();
            Container.BindSignal<EnemyKilledSignal>().ToMethod(() => Debug.Log("Fired EnemyKilledSignal"));
        }
    }

}
