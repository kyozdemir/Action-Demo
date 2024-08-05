using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    public interface ICollectable
    {
        void Setup();
        void Collect(PlayerController player);
        CollectableModel GetCollectableData();
        GameObject GetGameObject();
        void OnDestroy();
    }
}
