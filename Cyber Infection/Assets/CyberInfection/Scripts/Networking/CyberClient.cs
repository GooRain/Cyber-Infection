using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace CyberInfection.Networking
{
    public class CyberClient : LoadBalancingClient
    {
        public void CallConnect()
        {
            AppId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime;  // set your app id here
            AppVersion = PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion;  // set your app version here

            if (!ConnectToRegionMaster("rue")) // can return false for errors
            {
                DebugReturn(DebugLevel.ERROR, "Can't connect to: " + this.CurrentServerAddress);
            }
        }
    }
}
