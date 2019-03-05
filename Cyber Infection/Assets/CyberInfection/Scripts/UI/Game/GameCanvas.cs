using CyberInfection.Constants;
using CyberInfection.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CyberInfection.UI.Game
{
    public class GameCanvas : MonoBehaviour
    {
        public void ExitGame()
        {
            NetworkManager.instance.LeaveRoom();
            SceneManager.LoadSceneAsync(SceneName.Menu);
        }
    }
}
