using CyberInfection.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CyberInfection.UI.Game
{
    public class GameCanvas : MonoBehaviour
    {
        public void LoadMenuScene()
        {
            SceneManager.LoadSceneAsync(SceneName.Menu);
        }
    }
}
