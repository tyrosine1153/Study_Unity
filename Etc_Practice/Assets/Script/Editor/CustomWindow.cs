using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomWindow : EditorWindow
{
    // SerializedObject : SerializedProperty들을 갖고있는 하나의 스크립트 인스턴스를 말함
    private Dictionary<SerializedObject, List<SerializedProperty>> targets = new Dictionary<SerializedObject, List<SerializedProperty>>();
    private bool _isFocused = false;
    
    [MenuItem("Window/Editor Practice/Custom Window")]
    public static void ShowWindow()
    {
        GetWindow<CustomWindow>("Custom Window");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Refresh"))
        {
            RefreshTargets();
        }

        foreach (var target in targets)
        {
            EditorGUI.BeginChangeCheck();
            {
                EditorGUILayout.LabelField(target.Key.targetObject.name, EditorStyles.boldLabel);
                EditorGUI.indentLevel++;
                foreach (var property in target.Value)
                {
                    EditorGUILayout.PropertyField(property);
                }
                EditorGUI.indentLevel--;
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            }
            if (EditorGUI.EndChangeCheck())
            {
                target.Key.ApplyModifiedProperties();
            }
        }
    }

    private void RefreshTargets()
    {
        targets.Clear();

        var customScripts = FindObjectsOfType<CustomScript>();

        if (customScripts != null)
        {
            foreach (var customScript in customScripts)
            {
                var serializedObject = new SerializedObject(customScript);
                var serializedProperties = new List<SerializedProperty>
                {
                    serializedObject.FindProperty(nameof(customScript.target)),
                    serializedObject.FindProperty(nameof(customScript.myName)),
                    serializedObject.FindProperty(nameof(customScript.myHp)),
                };

                targets.Add(serializedObject, serializedProperties);
            }
        }
    }

    #region Focus 값에 관계 없이 값 업데이트 하도록 하기
    private void Update()
    {
        if (_isFocused) return;
        // 포커스가 되어있지 않아도 자동으로 값을 업데이트를 해야 하기 때문에
        // 근데 그럼 이건 창이 닫혔을 때도 업데이트가 되나?
        try
        {
            foreach (var target in targets)
            {
                if(target.Key.targetObject == null)
                {
                    throw new NullReferenceException();
                }
                target.Key.Update();
            }
        }
        catch (NullReferenceException e)
        {
            RefreshTargets();
        }
        
        Repaint();
    }

    private void OnFocus()
    {
        _isFocused = true;

        foreach (var target in targets)
        {
            target.Key.Update();
        }
    }

    private void OnLostFocus()
    {
        _isFocused = false;
    }
    #endregion
}