using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Data.Generating
{
	[CreateAssetMenu(menuName = "Cyber Infection/Data/Generating Scenes Data")]
	public class GeneratingScenesData : SingletonScriptableObject<GeneratingScenesData>
	{
		[SerializeField] private string[] _generateOnScenes;

		public bool DoGenerate(string loadedScene)
		{
			return _generateOnScenes.Any(scene => scene.ToLower().Equals(loadedScene.ToLower()));
		}
	}
}