using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    public abstract class CollectableBase : MonoBehaviour, ICollectable
    {
        public Vector3 Position => transform.position;
        public abstract ScriptableObject CollectableData { get; set; }

        public abstract void Setup();
        public abstract void Collect(PlayerController player);
        public abstract void OnDestroy();

        public CollectableModel GetCollectableData()
        {
            return new CollectableModel(Position, GetType().ToString().Replace(Constants.Prefs.NAMESPACE_PREFIX, string.Empty), CollectableData);
        }

        public GameObject GetGameObject() => gameObject;
    }

    [System.Serializable]
    public class CollectableModel
    {
        public Vector3 Position;
        public string CollectableType;
        public ScriptableObject CollectableData;

        public CollectableModel(Vector3 position, string collectableType, ScriptableObject collectableData)
        {
            Position = position;
            CollectableType = collectableType;
            CollectableData = collectableData;
        }
    }
}
