using CyberInfection.Constants;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace CyberInfection
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        public PlayerHandler playerHandler;
        
        private void Awake()
        {
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
    }
}