using CyberInfection.UI.MainMenu;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace CyberInfection.Extension.Utility
{
    // Здесь хранятся кастомные сериализируемые словари

    [System.Serializable]
    public class PaneltypeGameObjectDictionary : SerializableDictionaryBase<LobbyMainPanel.PanelType, GameObject>
    {
    }

    [System.Serializable]
    public class EnemyPrefabsDictionary : SerializableDictionaryBase<Enums.EnemyDifficulty, EnemiesPrefabs>
    {
    }

    [System.Serializable]
    public class KeyCodesDictionary : SerializableDictionaryBase<string, KeyCode>
    {
    }
}