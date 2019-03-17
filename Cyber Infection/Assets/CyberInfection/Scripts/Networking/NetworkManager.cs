using System;
using System.Collections.Generic;
using CyberInfection.Constants;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace CyberInfection.Networking
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public const string GameVersion = "0.0.1";
        
        public static NetworkManager instance { get; private set; }

        public static CyberClient client { get; private set; }

        public bool isConnectingToMaster { get; private set; }
        public bool isConnectionToRoom { get; private set; }

        private Action m_ConnectToMasterCallback;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void CreateNetworkManager()
        {
            new GameObject("Network Manager").AddComponent<NetworkManager>();
        }
        
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
            PhotonNetwork.SendRate = 60;
            PhotonNetwork.SerializationRate = 60;
        }

        private void Start()
        {
            isConnectingToMaster = false;
            isConnectionToRoom = false;
        }

        public void ConnectToMaster(string nickName, Action callback = null)
        {
            PhotonNetwork.OfflineMode = false;
            PhotonNetwork.NickName = nickName;
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = GameVersion;

            isConnectingToMaster = true;
            PhotonNetwork.ConnectUsingSettings();

            m_ConnectToMasterCallback = callback;
        }

        public void JoinRandomRoom()
        {
            if (!PhotonNetwork.IsConnected)
            {
                return;
            }

            isConnectionToRoom = true;
            PhotonNetwork.JoinRandomRoom();
        }
        
        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            isConnectionToRoom = false;
            isConnectingToMaster = false;
            Debug.Log(cause);
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            isConnectingToMaster = false;
            Debug.Log("Connected to Master!");

            m_ConnectToMasterCallback?.Invoke();
            m_ConnectToMasterCallback = null;
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            isConnectionToRoom = false;
            Debug.Log("Master: " + PhotonNetwork.IsMasterClient + " | Players In Room: " +
                      PhotonNetwork.CurrentRoom.PlayerCount);
            if (PhotonNetwork.IsMasterClient)
            {
                SceneManager.LoadScene(SceneName.Play);
            }
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);
            CreateRoom();
        }

        public void CreateRoom()
        {
            PhotonNetwork.CreateRoom(null, new RoomOptions
            {
                MaxPlayers = 4
            });
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public void Disconnect()
        {
            PhotonNetwork.Disconnect();
        }
    }
}