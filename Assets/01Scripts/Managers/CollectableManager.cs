using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ActionDemo
{
    public class CollectableManager : MonoBehaviour
    {
        [SerializeField] private CollectablePoolObject collectablePoolObject;
        [SerializeField] private List<CollectableModel> collectableDataList = new List<CollectableModel>();
        [SerializeField] private Transform collectableParent;

        public static CollectableManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            CreateCollectablePool();
        }

        private void Start()
        {
            InitializeCollectables();
        }

        private void CreateCollectablePool()
        {
            PoolManager.Instance.CreatePool<CollectablePoolObject>(Constants.PoolKeys.COLLECTABLE, collectablePoolObject, 10);
        }

        private void InitializeCollectables()
        {
            foreach (CollectableModel item in collectableDataList)
            {
                CollectablePoolObject collectable = PoolManager.Instance.GetObject<CollectablePoolObject>(
                    Constants.PoolKeys.COLLECTABLE,
                    item.Position,
                    Vector3.zero,
                    collectableParent
                );

                CollectableBase collectableBase = null;

                if (item.CollectableType == nameof(WeaponCollectable))
                {
                    collectableBase = collectable.gameObject.AddComponent<WeaponCollectable>();
                }
                else if (item.CollectableType == nameof(AttachmentCollectable))
                {
                    collectableBase = collectable.gameObject.AddComponent<AttachmentCollectable>();
                }

                if (collectableBase is not null)
                {
                    collectableBase.CollectableData = item.CollectableData;
                    collectableBase.Setup();
                }
            }
        }

        //Let's go with some data oriented approach.
        //This method works when the editor button is clicked.
        //This method can also be run before build.
        //While I use easy solutions where necessary, I do not hesitate to produce complex solutions.
        public void GetCollectablesOnScene()
        {
            ICollectable[] collectables = FindObjectsOfType<MonoBehaviour>().OfType<ICollectable>().ToArray();
            collectableDataList.Clear();

            foreach (ICollectable collectable in collectables)
            {
                collectableDataList.Add(collectable.GetCollectableData());
                DestroyImmediate(collectable.GetGameObject());
            }
        }
    }
}
