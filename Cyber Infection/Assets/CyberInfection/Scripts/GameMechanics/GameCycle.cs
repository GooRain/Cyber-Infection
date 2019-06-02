using CyberInfection.Constants;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CyberInfection.GameMechanics
{
    internal static class GameCycle
    {
        public static void CompleteLevel()
        {
            if (PhotonNetwork.OfflineMode)
            {
                SceneManager.LoadScene(SceneName.Play);
            }
            else
            {
                PhotonNetwork.LoadLevel(SceneName.Play);
            }
        }
    }
}