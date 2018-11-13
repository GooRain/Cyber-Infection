using Data.Settings.Generation;
using Generation.Map;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "MapGenerationInstaller", menuName = "Installers/MapGenerationInstaller")]
    public class MapGenerationInstaller : ScriptableObjectInstaller<MapGenerationInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Map>().AsTransient();
            Container.Bind<MapSettingsData>().AsCached();
        }
    }
}