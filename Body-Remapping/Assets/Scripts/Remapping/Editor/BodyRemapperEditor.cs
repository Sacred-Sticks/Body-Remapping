using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AvatarMapper))]
public class BodyRemapperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        AvatarMapper remapper = (AvatarMapper)target;
        if (GUILayout.Button("Read Proportions"))
        {
            remapper.MeasureAvatar();
        }
    }
}
