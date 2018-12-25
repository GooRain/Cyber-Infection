using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.MainMenu
{
	public class MenuCanvas : MonoBehaviour
	{
		public void LoadPlayScene()
		{
			SceneManager.LoadSceneAsync(SceneName.Play);
		}
		
	}
}
