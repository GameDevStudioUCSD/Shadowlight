using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Theme))]
public class ThemeEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        Theme theme = (Theme)target;
        if (GUILayout.Button("Apply Theme")) {
            theme.Apply();
        }
    }
}
