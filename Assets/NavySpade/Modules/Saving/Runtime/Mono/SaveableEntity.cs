using System;
using System.Collections.Generic;
using NavySpade.Modules.Saving.Runtime.Interfaces;
using UnityEditor;
using UnityEngine;

namespace NavySpade.Modules.Saving.Runtime.Mono
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        [field: SerializeField] public string _id = string.Empty;

        public string Id => _id;

        private static readonly Dictionary<string, SaveableEntity> GlobalLookup = new Dictionary<string, SaveableEntity>();

        public object CaptureState()
        {
            // Create a dictionary to store save data for this GameObject.
            var state = new Dictionary<string, object>();

            // Store all the save data from the saveable components on this GameObject.
            foreach (var saveable in GetComponents<ISaveable>())
            {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }

            // Return the save data to be saved to disk.
            return state;
        }

        public void RestoreState(object state)
        {
            var stateDictionary = (Dictionary<string, object>)state;
            
            foreach (var saveable in GetComponents<ISaveable>())
            {
                var typeName = saveable.GetType().ToString();
                
                if (stateDictionary.TryGetValue(typeName, out var value))
                {
                    saveable.RestoreState(value);
                }
            }
        }

#if UNITY_EDITOR

        private void Update()
        {
            if (Application.IsPlaying(gameObject))
            {
                return;
            }

            if (string.IsNullOrEmpty(gameObject.scene.path))
            {
                return;
            }

            var serializedObject = new SerializedObject(this);
            var property = serializedObject.FindProperty("_id");

            if (string.IsNullOrEmpty(property.stringValue) || !IsUnique(property.stringValue))
            {
                property.stringValue = Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }

            GlobalLookup[property.stringValue] = this;
        }

#endif

        private bool IsUnique(string candidate)
        {
            if (!GlobalLookup.ContainsKey(candidate))
            {
                return true;
            }

            if (GlobalLookup[candidate] == this)
            {
                return true;
            }

            if (GlobalLookup[candidate] == null)
            {
                GlobalLookup.Remove(candidate);
                return true;
            }

            if (GlobalLookup[candidate].Id != candidate)
            {
                GlobalLookup.Remove(candidate);
                return true;
            }

            return false;
        }
    }
}