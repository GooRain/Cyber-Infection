using CyberInfection.Extension.Utility;
using UnityEditor;

[CustomPropertyDrawer(typeof(PaneltypeGameObjectDictionary))]
public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}

public class AnySerializableDictionaryStoragePropertyDrawer: SerializableDictionaryStoragePropertyDrawer {}