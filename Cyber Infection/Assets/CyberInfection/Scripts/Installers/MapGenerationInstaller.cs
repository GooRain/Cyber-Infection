using CyberInfection.Data.Settings.Generation;
using CyberInfection.Generation;
using Plugins.Zenject.Source.Install;
using UnityEngine;

namespace CyberInfection.Installers
{
    [CreateAssetMenu(fileName = "MapGenerationInstaller", menuName = "Installers/MapGenerationInstaller")]
    public class MapGenerationInstaller : ScriptableObjectInstaller<MapGenerationInstaller>
    {
        [SerializeField] private MapSettingsData _mapSettingsData;
        
        public override void InstallBindings()
        {
            Container.Bind<Map>().AsTransient();
            Container.Bind<MapSettingsData>().FromInstance(_mapSettingsData);
        }
    }
}