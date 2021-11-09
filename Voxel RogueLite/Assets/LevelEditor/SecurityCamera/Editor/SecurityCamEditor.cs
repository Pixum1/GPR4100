using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SecurityCamera))]
public class SecurityCamEditor : Editor
{
    SerializedProperty left;
    SerializedProperty right;

    private void OnEnable()
    {
        left = serializedObject.FindProperty("rotLeft");
        right = serializedObject.FindProperty("rotRight");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        SecurityCamera cam = (SecurityCamera)target;

        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Left Rotation"))
        {
            left.vector3Value = cam.GetRotation();
        }
        if (GUILayout.Button("Right Rotation"))
        {
            right.vector3Value = cam.GetRotation();
        }
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }
}
