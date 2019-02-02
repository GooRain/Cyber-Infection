using CyberInfection.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CyberInfection.UI.MainMenu
{
	public class MenuCanvas : MonoBehaviour
	{
		public void LoadPlayScene()
		{
			SceneManager.LoadSceneAsync(SceneName.Play);
		}
		
	}
}
