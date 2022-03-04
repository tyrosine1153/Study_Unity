using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class AssetDatabaseTest : EditorWindow
{
    [MenuItem("Window/Editor Practice/Asset Database Test")]
    public static void ShowWindow()
    {
        GetWindow<AssetDatabaseTest>("Asset Database Test");
    }
    
    private void OnGUI()
    {
        if (GUILayout.Button("모든 Material 찾기"))
        {
            var guids = AssetDatabase.FindAssets("t:Material");
            
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                Debug.Log($"GUID : {guid}, Path : {path}, GUID from Path : {AssetDatabase.AssetPathToGUID(path)}");
                Debug.Log("===============================");
            }
        }

        if (GUILayout.Button("Material Asset 생성하기"))
        {
            var material = new Material(Shader.Find("Standard"));
            AssetDatabase.CreateAsset(material, $"Assets/Resources/EditorTest/MyMaterial{Random.Range(0, 1000)}.mat");
        }
        
        if (GUILayout.Button("모든 Material 로드 및 적용"))
        {
            var renderers = FindObjectsOfType<Renderer>();
            foreach (var renderer in renderers)
            {
                DestroyImmediate(renderer.gameObject);
            }
            
            var guids = AssetDatabase.FindAssets("t:Material");

            for (int i = 0; i < guids.Length; i++)
            {
                var guid = guids[i];
                var path = AssetDatabase.GUIDToAssetPath(guid);
                
                var material = AssetDatabase.LoadAssetAtPath<Material>(path);
                if (material != null)
                {
                    Debug.Log($"Material Loaded : {path}");

                    var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    cube.transform.position = new Vector3(i * 2, 0, 0);
                    cube.GetComponent<Renderer>().material = material;
                }
            }
        }
    }
}
