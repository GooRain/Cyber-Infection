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
    public class LobbyMainPanel : MonoBehaviour
    {
        [System.Serializable]
        public enum PanelType : sbyte
        {
            Main = 0,

            Single = 16,

            Multi = 32,
            Lobby = 33,
            CreateLobby = 34,
            JoinLobby = 35
        }

        public PaneltypeGameObjectDictionary panelDictionary;

        #region SINGLEPLAYER

        public void OnButtonSingleplayerClicked()
        {
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
            SetPanel(PanelType.Multi);
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
            SetPanel(PanelType.Main);

        }

        public void OnButtonQuitClicked()
        {
            Application.Quit();
        }

        public void SetPanel(PanelType type)
        {
            if (panelDictionary.ContainsKey(type))
            {
                foreach (var panel in panelDictionary)
                {
                    panel.Value.SetActive(false);
                }

                panelDictionary[type].SetActive(true);
            }
        }
    }
}