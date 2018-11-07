using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.MainMenu
{
	public class MenuCanvas : MonoBehaviour
	{
		public void OnNextSceneButtonClick(string sceneName)
		{
			SceneManager.LoadScene(sceneName);
		}
		
	}
}
