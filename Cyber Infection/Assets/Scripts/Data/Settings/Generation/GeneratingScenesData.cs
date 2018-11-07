using System.Linq;
using Data.Settings.Base;
using UnityEngine;

namespace Data.Settings.Generation
{
	[CreateAssetMenu(menuName = "Cyber Infection/Data/Generating Scenes Data")]
	public class GeneratingScenesData : SettingsDataBase<GeneratingScenesData>
	{
		private const string AssetPath = "Data/Settings/GeneratingScenesData";
		public override GeneratingScenesData GetCopy()
		{
			return Instantiate(TryToLoad(AssetPath));
		}
		
		[SerializeField] private string[] _generateOnScenes;

		public bool DoGenerate(string loadedScene)
		{
			return _generateOnScenes.Any(scene => scene.ToLower().Equals(loadedScene.ToLower()));
		}
	}
}