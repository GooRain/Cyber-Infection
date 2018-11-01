using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Data.Generating
{
	public class GeneratingScenesData : SingletonScriptableObject<GeneratingScenesData>
	{
		[SerializeField] private Scene[] _generateOnScenes;

		public bool DoGenerate(Scene loadedScene)
		{
			return _generateOnScenes.Any(scene => loadedScene.name.Equals(scene.name));
		}
	}
}