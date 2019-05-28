using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace CyberInfection.Generation.Room
{
    [System.Serializable]
    public class RoomTypeTemplatesDictionary : SerializableDictionaryBase<RoomType, Texture2DListHolder>
    {
        public Texture2D GetRandomTemplate(RoomType type)
        {
            return this[type].GetRandom();
        }
    }

    [System.Serializable]
    public class Texture2DListHolder
    {
        [SerializeField] private List<Texture2D> textures;

        public List<Texture2D> Textures => textures;

        public Texture2D GetRandom()
        {
            return textures.Count < 1 ? null : textures[Random.Range(0, textures.Count)];
        }
    }
}