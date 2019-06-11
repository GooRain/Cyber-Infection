using System.Collections.Generic;
using CyberInfection.Generation.Tiles;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace CyberInfection.Generation.Room
{
    public class DoorDirDoorsDictionary : SerializableDictionaryBase<DoorDir, List<Vector3Int>>
    {
        public Vector3Int GetRandomDoor(DoorDir dir)
        {
            var list = this[dir];
            return list.Count < 1
                ? Vector3Int.zero 
                : list[Random.Range(0, list.Count)];
        }
    }
}