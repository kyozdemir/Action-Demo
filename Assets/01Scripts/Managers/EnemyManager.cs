using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    public class EnemyManager : CharacterManager
    {
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private List<EnemyData> enemyDatas = new List<EnemyData>();
        [SerializeField] private Transform enemyParent;

        public static EnemyManager Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            CreateEnemyPool();
        }

        private void Start()
        {
            InitializeEnemies();
        }

        private void InitializeEnemies()
        {
            foreach (EnemyData item in enemyDatas)
            {
                Enemy enemy = PoolManager.Instance.GetObject<Enemy>(
                    Constants.PoolKeys.ENEMY,
                    item.Position,
                    Vector3.zero,
                    enemyParent
                );

                enemy.WeaponReference = item.WeaponSO;
                enemy.Initialize();
                enemy.OnDeaht += OnDeath;
            }
        }

        private void CreateEnemyPool()
        {
            PoolManager.Instance.CreatePool<Enemy>(Constants.PoolKeys.ENEMY, enemyPrefab, 3);
        }

        public void GetEnemiesOnScene()
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            enemyDatas.Clear();

            foreach (Enemy item in enemies)
            {
                enemyDatas.Add(new EnemyData(item.transform.position, item.WeaponReference));
                DestroyImmediate(item.gameObject);
            }
        }
    }

    [System.Serializable]
    public class EnemyData
    {
        public Vector3 Position;
        public WeaponSO WeaponSO;

        public EnemyData(Vector3 position, WeaponSO weaponSO)
        {
            Position = position;
            WeaponSO = weaponSO;
        }
    }
}
