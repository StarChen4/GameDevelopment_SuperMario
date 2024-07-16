using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PrefabScaler : EditorWindow
{
    private float scaleFactor = 6.25f; // 1 / 0.16 = 6.25

    [MenuItem("Tools/Prefab Scaler")]
    public static void ShowWindow()
    {
        GetWindow<PrefabScaler>("Prefab Scaler");
    }

    void OnGUI()
    {
        GUILayout.Label("Scale Prefabs", EditorStyles.boldLabel);
        scaleFactor = EditorGUILayout.FloatField("Scale Factor", scaleFactor);

        if (GUILayout.Button("Scale Selected Prefabs"))
        {
            ScaleSelectedPrefabs();
        }
    }

    void ScaleSelectedPrefabs()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            string prefabPath = AssetDatabase.GetAssetPath(obj);
            GameObject prefab = PrefabUtility.LoadPrefabContents(prefabPath);

            prefab.transform.localScale *= scaleFactor;

            PrefabUtility.SaveAsPrefabAsset(prefab, prefabPath);
            PrefabUtility.UnloadPrefabContents(prefab);
        }
    }
}
