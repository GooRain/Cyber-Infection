using System.Collections.Generic;
using System.Linq;
using CyberInfection.GameMechanics.Camera;
using CyberInfection.Generation.Room;
using UnityEngine;

namespace CyberInfection.GameMechanics
{
    public class Level : MonoBehaviour
    {

        private List<RoomController> roomControllers = new List<RoomController>();
        
        public bool IsComplete
        {
            get
            {
                return roomControllers.All(r => r.isCompleted);
            }
        }
        
        public RoomController currentRoomController { get; private set; }
        
        public void SelectRoomController(RoomController roomController)
        {
            if (currentRoomController != null)
            {
                currentRoomController.TryToToggle(false);
                Debug.Log("Hiding -> " + currentRoomController.name);
            }
            
            currentRoomController = roomController;
            Debug.Log("Selecting -> " + roomController.name);
            currentRoomController.TryToToggle(true);
            
            CameraController.Instance.SetRoom(currentRoomController);
        }

        public void AddRoom(RoomController newRoomController)
        {
            roomControllers.Add(newRoomController);
        }
    }
}