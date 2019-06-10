using System.Collections.Generic;
using CyberInfection.Generation.Tiles;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace CyberInfection.Generation.Room
{
    public class DoorDirDoorsDictionary : SerializableDictionaryBase<DoorDir, List<Door>>
    {
        public Door GetRandomDoor(DoorDir dir)
        {
            var list = this[dir];
            return list.Count < 1
                ? new GameObject("NULL DOOR").AddComponent<Door>()
                : list[Random.Range(0, list.Count)];
        }
    }
}