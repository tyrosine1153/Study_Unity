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
        _customScript = (CustomScript) target;

        _targetProperty = serializedObject.FindProperty(nameof(_customScript.target));
        _myNameProperty = serializedObject.FindProperty(nameof(_customScript.myName));
        _myHpProperty = serializedObject.FindProperty(nameof(_customScript.myHp));
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_targetProperty);

        if (_myHpProperty.intValue < 30)
        {
            GUI.color = Color.red;
        }
        else
        {
            GUI.color = Color.green;
        }
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
}
