using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CustomScript))]
public class CustomEditorTest : Editor
{
    private CustomScript _customScript;

    private SerializedProperty _targetProperty;
    private SerializedProperty _myNameProperty;
    private SerializedProperty _myHpProperty;

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
        _customScript = (CustomScript) target;

        _targetProperty = serializedObject.FindProperty(nameof(_customScript.target));
        _myNameProperty = serializedObject.FindProperty(nameof(_customScript.myName));
        _myHpProperty = serializedObject.FindProperty(nameof(_customScript.myHp));
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_targetProperty);

        GUI.color = _myHpProperty.intValue < 30 ? Color.red : Color.green;
        _myHpProperty.intValue = EditorGUILayout.IntSlider("HP 값", _myHpProperty.intValue, 0, 100);

        EditorGUILayout.BeginHorizontal();
        {
            GUI.color = Color.yellow;
            EditorGUILayout.PrefixLabel("이름");
            GUI.color = Color.white;
            _myNameProperty.stringValue = EditorGUILayout.TextArea(_myNameProperty.stringValue);
        }
        EditorGUILayout.EndHorizontal();
        
        serializedObject.ApplyModifiedProperties();
    }
    
    private void OnSceneGUI(SceneView sceneView)
    {
        Handles.Label(_customScript.transform.position, _customScript.gameObject.name);

        Handles.color = Color.red;
        var customScripts = FindObjectsOfType<CustomScript>();
        foreach (var customScript in customScripts)
        {
            if (_customScript == customScript) continue;
            
            var position = customScript.transform.position;
            Handles.DrawLine(_customScript.transform.position, position);
                
            Handles.DrawWireCube(position, Vector3.one);
        }
        
        Handles.color = Color.green;
        Handles.DrawWireCube(_customScript.transform.position, Vector3.one * 2);
        Handles.color = Color.white;
        
        Handles.BeginGUI();
        if(GUILayout.Button("Move Right"))
        {
            _customScript.transform.position += Vector3.right;
        }
        if(GUILayout.Button("Move Left"))
        {
            _customScript.transform.position += Vector3.left;
        }
        Handles.EndGUI();
    }
}
