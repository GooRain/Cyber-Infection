﻿using System;
using System.Collections.Generic;
using Data;
using UnityEditor;
using UnityEngine;
using UnityObject = UnityEngine.Object;
 
namespace Editor
{
    public abstract class DictionaryDrawer<TKey, TValue> : PropertyDrawer
    {
        private SerializableDictionary<TKey, TValue> _dictionary;
        private bool _foldout;
        private const float KButtonWidth = 18f;
 
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            CheckInitialize(property, label);
            if (_foldout)
                return (_dictionary.Count + 1) * 17f;
            return 17f;
        }
 
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            CheckInitialize(property, label);
 
            position.height = 17f;
 
            var foldoutRect = position;
            foldoutRect.width -= 2 * KButtonWidth;
            EditorGUI.BeginChangeCheck();
            _foldout = EditorGUI.Foldout(foldoutRect, _foldout, label, true);
            if (EditorGUI.EndChangeCheck())
                EditorPrefs.SetBool(label.text, _foldout);
 
            var buttonRect = position;
            buttonRect.x = position.width - KButtonWidth + position.x;
            buttonRect.width = KButtonWidth + 2;
 
            if (GUI.Button(buttonRect, new GUIContent("+", "Add item"), EditorStyles.miniButton))
            {
                AddNewItem();
            }
 
            buttonRect.x -= KButtonWidth;
 
            if (GUI.Button(buttonRect, new GUIContent("X", "Clear dictionary"), EditorStyles.miniButtonRight))
            {
                ClearDictionary();
            }
 
            if (!_foldout)
                return;
 
            foreach (var item in _dictionary)
            {
                var key = item.Key;
                var value = item.Value;
 
                position.y += 17f;
 
                var keyRect = position;
                keyRect.width /= 2;
                keyRect.width -= 4;
                EditorGUI.BeginChangeCheck();
                var newKey = DoField(keyRect, typeof(TKey), key);
                if (EditorGUI.EndChangeCheck())
                {
                    try
                    {
                        _dictionary.Remove(key);
                        _dictionary.Add(newKey, value);
                    }
                    catch(Exception e)
                    {
                        Debug.Log(e.Message);
                    }
                    break;
                }
 
                var valueRect = position;
                valueRect.x = position.width / 2 + 15;
                valueRect.width = keyRect.width - KButtonWidth;
                EditorGUI.BeginChangeCheck();
                value = DoField(valueRect, typeof(TValue), value);
                if (EditorGUI.EndChangeCheck())
                {
                    _dictionary[key] = value;
                    break;
                }
 
                var removeRect = valueRect;
                removeRect.x = valueRect.xMax + 2;
                removeRect.width = KButtonWidth;
                if (GUI.Button(removeRect, new GUIContent("x", "Remove item"), EditorStyles.miniButtonRight))
                {
                    RemoveItem(key);
                    break;
                }
            }
        }
 
        private void RemoveItem(TKey key)
        {
            _dictionary.Remove(key);
        }
 
        private void CheckInitialize(SerializedProperty property, GUIContent label)
        {
            if (_dictionary == null)
            {
                var target = property.serializedObject.targetObject;
                _dictionary = fieldInfo.GetValue(target) as SerializableDictionary<TKey, TValue>;
                if (_dictionary == null)
                {
                    _dictionary = new SerializableDictionary<TKey, TValue>();
                    fieldInfo.SetValue(target, _dictionary);
                }
 
                _foldout = EditorPrefs.GetBool(label.text);
            }
        }
 
        private static readonly Dictionary<Type, Func<Rect, object, object>> _Fields =
            new Dictionary<Type,Func<Rect,object,object>>()
            {
                { typeof(int), (rect, value) => EditorGUI.IntField(rect, (int)value) },
                { typeof(float), (rect, value) => EditorGUI.FloatField(rect, (float)value) },
                { typeof(string), (rect, value) => EditorGUI.TextField(rect, (string)value) },
                { typeof(bool), (rect, value) => EditorGUI.Toggle(rect, (bool)value) },
                { typeof(Vector2), (rect, value) => EditorGUI.Vector2Field(rect, GUIContent.none, (Vector2)value) },
                { typeof(Vector3), (rect, value) => EditorGUI.Vector3Field(rect, GUIContent.none, (Vector3)value) },
                { typeof(Bounds), (rect, value) => EditorGUI.BoundsField(rect, (Bounds)value) },
                { typeof(Rect), (rect, value) => EditorGUI.RectField(rect, (Rect)value) },
            };
 
        private static T DoField<T>(Rect rect, Type type, T value)
        {
            Func<Rect, object, object> field;
            if (_Fields.TryGetValue(type, out field))
                return (T)field(rect, value);
 
            if (type.IsEnum)
                return (T)(object)EditorGUI.EnumPopup(rect, (Enum)(object)value);
 
            if (typeof(UnityObject).IsAssignableFrom(type))
                return (T)(object)EditorGUI.ObjectField(rect, (UnityObject)(object)value, type, true);
 
            Debug.Log("Type is not supported: " + type);
            return value;
        }
 
        private void ClearDictionary()
        {
            _dictionary.Clear();
        }
 
        private void AddNewItem()
        {
            TKey key;
            if (typeof(TKey) == typeof(string))
                key = (TKey)(object)"";
            else key = default(TKey);
 
            var value = default(TValue);
            try
            {
                _dictionary.Add(key, value);
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }
}