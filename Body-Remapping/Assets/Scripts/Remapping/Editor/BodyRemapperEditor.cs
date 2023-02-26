using Remapping;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(AvatarMapper))]
public class BodyRemapperEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var remapper = (AvatarMapper)target;
        if (GUILayout.Button("Read Proportions")) {
            remapper.MeasureAvatar();
        }
    }

}
