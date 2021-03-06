using CyberInfection.Generation.Room;
using UnityEngine;

namespace CyberInfection.Data.Settings.Generation
{
    [CreateAssetMenu(menuName = "Cyber Infection/Data/Room Type Templates Data")]
    public class RoomTypeTemplatesData : ScriptableObject
    {
        [SerializeField] private RoomTypeTemplatesDictionary roomTypeTemplatesDictionary;
        
        public Texture2D GetRandom(RoomType roomType)
        {
            return roomTypeTemplatesDictionary.GetRandomTemplate(roomType);
        }
    }
}