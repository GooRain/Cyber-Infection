using CyberInfection.Constants;
using CyberInfection.GameMechanics.Camera;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CyberInfection
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        public PlayerHandler playerHandler;
        
        private void Awake()
        {
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