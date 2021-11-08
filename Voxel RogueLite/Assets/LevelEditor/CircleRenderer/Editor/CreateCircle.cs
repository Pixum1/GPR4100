using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DrawCircle))]
public class CreateCircle : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DrawCircle drawCircle = (DrawCircle)target;

        if(GUILayout.Button("Create Circle"))
        {
            drawCircle.Create();
        }
    }
}
