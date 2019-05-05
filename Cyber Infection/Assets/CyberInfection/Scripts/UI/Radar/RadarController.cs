using UnityEngine;

namespace CyberInfection.UI.Radar
{
    public class RadarController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _room;
        [SerializeField]
        private GameObject _minichar;

        private int count = 1; //room count
        private static int maxcount;

        private Transform lastRoomTransform;
        private Vector3 sidePosition;

        private GameObject minicharObj;
        private GameObject[] allRadarRooms;
        

        public static RadarController instance { get; private set; }

        public void DrawRoom(int roomSide)
        {
            return; // TODO: Айаал, тут надо что-то сделать, куча багов
            // 1 left
            // 2 right
            // 3 down
            // 4 up
            bool visit= false;
            int visitRoom = 0;

            switch (roomSide)
            {
                case 1:
                    sidePosition = new Vector3(-0.8f, 0, 0);
                    break;
                case 2:
                    sidePosition = new Vector3(0.8f, 0, 0);
                    break;
                case 3:
                    sidePosition = new Vector3(0, -0.5f, 0);
                    break;
                case 4:
                    sidePosition = new Vector3(0, 0.5f, 0);
                    break;
                default:
                    Debug.Log("ERROR IN DRAW ROOM roomSide nubmer");
                    break;
            }
            for (int i = 0; i < count; i++)
            {
                if (i >= allRadarRooms.Length)
                {
                    break; // TODO: Айаал, тут надо что-то сделать, чтобы за пределы массива не уходило
                }
                
                if (allRadarRooms[i].transform.position == lastRoomTransform.position + sidePosition)
                {
                    if (allRadarRooms[i].transform.position == lastRoomTransform.position + sidePosition)
                    {
                        visit = true;
                        visitRoom = i;
                        break;
                        
                    }
                    else visit = false;
                }
                else visit = false;
            }

            if (visit)
            {
                lastRoomTransform = allRadarRooms[visitRoom].transform;
                minicharObj.transform.position = allRadarRooms[visitRoom].transform.position;
            }
            else
            {
                
                GameObject roomObj = Instantiate(_room, lastRoomTransform.position + sidePosition, Quaternion.identity) as GameObject;
                count++;
                roomObj.transform.parent = this.transform;
                roomObj.name = "room " + count;
                allRadarRooms[count-1] = roomObj;
                lastRoomTransform = roomObj.transform;
                minicharObj.transform.position = roomObj.transform.position;
            }
            RadarCamera.instance.LookMinichar(minicharObj);
        }
        
        void Start()
        {
            allRadarRooms = new GameObject[maxcount];
            GameObject roomObj = Instantiate(_room, transform.position, Quaternion.identity) as GameObject;
            allRadarRooms[0] = roomObj;
            roomObj.transform.parent = this.transform;
            roomObj.name = "room 1";

            lastRoomTransform = roomObj.transform;
            minicharObj = Instantiate(_minichar, roomObj.transform.position, Quaternion.identity) as GameObject;
            minicharObj.transform.parent = this.transform;
        }
        private void Awake()
        {
            instance = this;
            
        }

        public void SetRoomsCount(int roomCount)
        {
            maxcount = roomCount + 1;
        }

        private void Clear()
        {
            for (int i = 0; i < maxcount; i++)
            {
                Destroy(allRadarRooms[i]);
                allRadarRooms[i] = null;
            }
            Destroy(minicharObj);
        }
    }
}
