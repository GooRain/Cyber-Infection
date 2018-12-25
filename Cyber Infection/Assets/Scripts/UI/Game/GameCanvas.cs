using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Game
{
    public class GameCanvas : MonoBehaviour
    {
        public void LoadMenuScene()
        {
            SceneManager.LoadSceneAsync(SceneName.Menu);
        }
    }
}
