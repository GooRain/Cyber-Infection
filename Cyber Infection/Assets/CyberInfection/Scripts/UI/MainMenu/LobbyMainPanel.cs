using System.Collections.Generic;
using CyberInfection.Constants;
using CyberInfection.Data;
using CyberInfection.Extension.Utility;
using CyberInfection.Networking;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CyberInfection.UI.MainMenu
{
    public class LobbyMainPanel : MonoBehaviourPunCallbacks
    {
        [System.Serializable]
        public enum PanelType : sbyte
        {
            Main = 0,

            Single = 16,

            Multi = 32,
            Lobby = 33,
            CreateLobby = 34,
            JoinLobby = 35,
            
            Loading = 64
        }

        public PaneltypeGameObjectDictionary panelDictionary;

        private PanelType m_CurrentPanel;
        
        #region SINGLEPLAYER

        public void OnButtonSingleplayerClicked()
        {
            PhotonNetwork.OfflineMode = true;
            SetPanel(PanelType.Single);
        }

        public void OnButtonStartClicked()
        {
            SceneManager.LoadSceneAsync(SceneName.Play);
        }

        public void OnButtonContinueClicked()
        {
        }

        #endregion


        #region MULTIPLAYER

        public void OnButtonMultiplayerClicked()
        {
            PhotonNetwork.OfflineMode = false;
            NetworkManager.instance.ConnectToMaster("Test_" + Random.Range(1000, 9999));
            SetPanel(PanelType.Loading);
        }

        public void OnButtonCreateClicked()
        {
            NetworkManager.instance.CreateRoom();
        }

        public void OnButtonJoinClicked()
        {
            NetworkManager.instance.JoinRandomRoom();
        }

        #endregion

        public void OnButtonBackClicked()
        {
            if (m_CurrentPanel == PanelType.Multi)
            {
                NetworkManager.instance.Disconnect();
            }
            
            SetPanel(PanelType.Main);
        }

        public void OnButtonQuitClicked()
        {
            Application.Quit();
        }

        private void SetPanel(PanelType type)
        {
            if (panelDictionary.ContainsKey(type))
            {
                foreach (var panel in panelDictionary)
                {
                    panel.Value.SetActive(false);
                }

                m_CurrentPanel = type;
                panelDictionary[m_CurrentPanel].SetActive(true);
            }
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            SetPanel(PanelType.Multi);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            SetPanel(PanelType.Main);
        }
    }
}