using UnityEngine;
using UnityEditor;

namespace ActionDemo
{
    [CustomEditor(typeof(CollectableManager))]
    public class CollectableManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CollectableManager collectableManager = (CollectableManager)target;

            if (GUILayout.Button("Get Collectable Data From Scene"))
            {
                collectableManager.GetCollectablesOnScene();
            }
        }
    }

}
