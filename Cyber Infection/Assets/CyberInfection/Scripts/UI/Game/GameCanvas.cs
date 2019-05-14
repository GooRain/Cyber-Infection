using CyberInfection.Constants;
using CyberInfection.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CyberInfection.UI.Game
{
    public class GameCanvas : MonoBehaviour
    {
        public static GameCanvas instance { get; private set; }

        [SerializeField] private Image _playerHealthBar;

        private void Awake()
        {
            instance = this;
        }

        public void OnPlayerHealthChanged(float percentage)
        {
            _playerHealthBar.fillAmount = percentage;
        }

        public void ExitGame()
        {
            NetworkManager.instance.LeaveRoom();
            SceneManager.LoadSceneAsync(SceneName.Menu);
        }

        private void OnDestroy()
        {
            instance = null;
        }
    }
}
