using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menu
{
	public class MenuCanvas : MonoBehaviour
	{

		[SerializeField]
		private Scene _nextScene;

		public void OnNextSceneButtonClick()
		{
			SceneManager.LoadScene(_nextScene.name);
		}
		
	}
}
