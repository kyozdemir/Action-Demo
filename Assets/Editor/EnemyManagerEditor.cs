using UnityEngine;
using UnityEditor;

namespace ActionDemo
{
    [CustomEditor(typeof(EnemyManager))]
    public class EnemyManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EnemyManager enemyManager = (EnemyManager)target;

            if (GUILayout.Button("Get Enemy Data From Scene"))
            {
                enemyManager.GetEnemiesOnScene();
            }
        }
    }
}
