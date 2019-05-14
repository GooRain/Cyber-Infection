using CyberInfection.GameMechanics.Camera;
using CyberInfection.Generation.Room;
using UnityEngine;

namespace CyberInfection.GameMechanics
{
    public class Level : MonoBehaviour
    {
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
            
            CameraController.instance.SetRoom(currentRoomController);
        }
    }
}