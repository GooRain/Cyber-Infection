using System;
using CyberInfection.Constants;
using CyberInfection.Generation.Room;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace CyberInfection.GameMechanics
{
    public class LevelController : MonoBehaviourPunCallbacks
    {
        public static LevelController instance;
        
        public PlayerHandler playerHandler;
        public Level level;

        public event Action<RoomController> RoomCompleteEvent = delegate { };
        
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

        private void CheckLevelCompleteness()
        {
            if (IsLevelComplete())
            {
                GameCycle.CompleteLevel();
            }
        }

        private bool IsLevelComplete()
        {
            return level.IsComplete;
        }

        private void OnDestroy()
        {
            instance = null;
        }

        public void RoomIsCompleted(RoomController roomController)
        {
            RoomCompleteEvent.Invoke(roomController);
        }
    }
}