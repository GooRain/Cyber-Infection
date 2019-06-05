using System.Collections.Generic;
using System.Linq;
using CyberInfection.GameMechanics.Camera;
using CyberInfection.Generation.Room;
using UnityEngine;

namespace CyberInfection.GameMechanics
{
    public class Level : MonoBehaviour
    {

        private readonly List<RoomController> roomControllers = new List<RoomController>();
        
        public bool IsComplete
        {
            get
            {
                //roomControllers.ForEach(
                //    r => Debug.Log("<color=red>" + r.name + " is completed = " + r.isCompleted + "</color>"));
                return roomControllers.All(r => r.isCompleted);
            }
        }
        
        public RoomController CurrentRoomController { get; private set; }
        
        public void SelectRoomController(RoomController roomController)
        {
            if (CurrentRoomController != null)
            {
                CurrentRoomController.TryToToggle(false);
                //Debug.Log("Hiding -> " + currentRoomController.name);
            }
            
            CurrentRoomController = roomController;
            //Debug.Log("Selecting -> " + roomController.name);
            CurrentRoomController.TryToToggle(true);
            
            CameraController.Instance.SetRoom(CurrentRoomController);
        }

        public void AddRoom(RoomController newRoomController)
        {
            roomControllers.Add(newRoomController);
        }
    }
}