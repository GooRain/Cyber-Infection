using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace CyberInfection.Networking
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public static NetworkManager instance { get; private set; }

        public static CyberClient client { get; private set; }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster");
            var roomOptions = new RoomOptions {IsVisible = false, MaxPlayers = 4};
            var creatingRoom = PhotonNetwork.JoinOrCreateRoom("Testing", roomOptions, TypedLobby.Default);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Joined!");
        }
    }
}