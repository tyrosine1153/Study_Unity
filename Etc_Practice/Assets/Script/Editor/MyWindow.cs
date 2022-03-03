using UnityEditor;
using UnityEngine;

public class MyWindow : EditorWindow
{
    [MenuItem("Window/My Window")]
    public static void ShowWindow()
    {
        var window = GetWindow<MyWindow>("My Window");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Hello World!", EditorStyles.boldLabel);
        
        var content = new GUIContent("Hello World!");
        content.image = EditorGUIUtility.FindTexture("BuildSettings.Editor");
        EditorGUILayout.LabelField(content);
        
        content.tooltip = "This is a tooltip";
        GUILayout.Button(content);
        
        var style = new GUIStyle(GUI.skin.button);
        style.fontSize = 15;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.green;
        style.hover.textColor = Color.red;
        
        GUILayout.Button(content, style);

        
    }
}
