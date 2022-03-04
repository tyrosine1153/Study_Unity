using UnityEditor;
using UnityEngine;

public class PrefsTest : EditorWindow
{
    private string stringvalue = "";
    private int intvalue = 0;

    [MenuItem("Window/Editor Practice/Prefs Test")]
    public static void ShowWindow()
    {
        GetWindow<PrefsTest>("Prefs Test");
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.PrefixLabel("String Value");
            stringvalue = EditorGUILayout.TextArea(stringvalue);
        }
        EditorGUILayout.EndHorizontal();
        intvalue = EditorGUILayout.IntField("Int Value", intvalue);

        if (GUILayout.Button("Save Prefs"))
        {
            EditorPrefs.SetString("string-value", stringvalue);
            EditorPrefs.SetInt("int-value", intvalue);
        }

        if (GUILayout.Button("Load Prefs"))
        {
            stringvalue = EditorPrefs.GetString("string-value");
            intvalue = EditorPrefs.GetInt("int-value");
        }

        if (GUILayout.Button("Delete Prefs"))
        {
            EditorPrefs.DeleteKey("string-value");
            EditorPrefs.DeleteKey("int-value");
        }
    }
}
