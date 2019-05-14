using CyberInfection.Constants;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace CyberInfection.GameMechanics
{
    public class LevelController : MonoBehaviourPunCallbacks
    {
        public static LevelController instance;
        
        public PlayerHandler playerHandler;
        public Level level;
        
        private void Awake()
        {
            instance = this;
            
            if (PhotonNetwork.OfflineMode)
            {
                return;
            }
            
            if (!PhotonNetwork.IsConnected)
            {
                SceneManager.LoadScene(SceneName.Menu);
            }
        }

        private void Start()
        {
            playerHandler.RefreshMinePlayer();
        }

        private void OnDestroy()
        {
            instance = null;
        }
    }
}