using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    public class CollectablePoolObject : PoolObject
    {
        public override void ResetObject()
        {
            CollectableBase collectableBase = GetComponent<CollectableBase>();
            if (collectableBase is not null)
                Destroy(collectableBase);
        }
    }
}
