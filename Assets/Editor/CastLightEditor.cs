using UnityEngine;
using System.Collections;
using UnityEditor;

//Modified from code written by Sebastian Lague
//Original Source: https://github.com/SebLague/Field-of-View/blob/master/Episode%2003/Editor/FieldOfViewEditor.cs

[CustomEditor(typeof(CastLight))]
public class FieldOfViewEditor : Editor {

    void OnSceneGUI() {
        CastLight fow = (CastLight)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.forward, Vector3.up, 360, fow.viewRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);
    }

}