using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorCopy : EditorWindow
{
    
    private Editor duplicatedEditor;  // 복제한 오브젝트 표시 에디터
    private Editor[] duplicatedEditorDetails;  // 복제한 컴포넌트 표시 에디터
    private List<bool> detailFoldout = new List<bool>();  // 토글 상태 저장

    [MenuItem("Window/Editor Practice/Editor Copy")]
    public static void ShowWindow()
    {
        GetWindow<EditorCopy>("Editor Copy");
    }

    private void OnGUI()
    {
        if (Selection.objects != null && Selection.objects.Length == 1)
        {
            var target = Selection.objects[0];
            if (duplicatedEditor == null || duplicatedEditor.name != target.name)
            {
                duplicatedEditor = Editor.CreateEditor(target);
                var gameObject = target as GameObject;
                if (gameObject != null)
                {
                    var components = gameObject.GetComponents<Component>();
                    if (components != null)
                    {
                        duplicatedEditorDetails = new Editor[components.Length];
                        for (int i = 0; i < components.Length; i++)
                        {
                            duplicatedEditorDetails[i] = Editor.CreateEditor(components[i]);
                            detailFoldout.Add(false);
                        }
                    }
                }
                else
                {
                    duplicatedEditorDetails = null;
                    detailFoldout.Clear();
                }
            }
        }
        else
        {
            duplicatedEditor = null;
            duplicatedEditorDetails = null;
            detailFoldout.Clear();
        }

        if (duplicatedEditor != null)
        {
            duplicatedEditor.DrawHeader();
            duplicatedEditor.OnInspectorGUI();

            if (duplicatedEditorDetails != null)
            {
                for (int i = 0; i < duplicatedEditorDetails.Length; i++)
                {
                    // Fold Out : 토글같은 거
                    detailFoldout[i] = EditorGUILayout.Foldout(detailFoldout[i], duplicatedEditorDetails[i].GetType().ToString());
                    if (detailFoldout[i])
                    {
                        duplicatedEditorDetails[i].OnInspectorGUI();
                    }
                }
            }
        }
    }
}