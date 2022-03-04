using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SelectionTest : EditorWindow
{
    [MenuItem("Window/Editor Practice/Selection Test")]
    public static void ShowWindow()
    {
        GetWindow<SelectionTest>("Selection Test");
    }
    
    private void OnGUI()
    {
        if (GUILayout.Button("모든 Scene Object 선택하기"))
        {
            var targets = FindObjectsOfType<GameObject>(true);
            if (targets != null)
            {
                Selection.objects = targets;
            }
        }
        
        if (GUILayout.Button("모든 Scene Object 선택해제하기"))
        {
            Selection.objects = new Object[0];
        }

        if (GUILayout.Button("모든 Text 선택하기"))
        {
            var targets = FindObjectsOfType<Text>(true);
            if(targets != null)
            {
                var gameObjects = new GameObject[targets.Length];
                for (int i = 0; i < targets.Length; i++)
                {
                    gameObjects[i] = targets[i].gameObject;
                }
                Selection.objects = gameObjects;
            }
        }
        
        if(GUILayout.Button("선택한 Object 삭제하기"))
        {
            foreach (var obj in Selection.objects)
            {
                DestroyImmediate(obj);
            }
        }
        
        if(GUILayout.Button("선택한 한 개의 Object 핑 찍기"))
        {
            if(Selection.objects != null && Selection.objects.Length == 1)
            {
                var obj = Selection.objects[0];
                EditorGUIUtility.PingObject(obj);
            }
        }
    }
}
