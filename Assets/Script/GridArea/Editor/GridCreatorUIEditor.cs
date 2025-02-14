using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridCreator))]
public class GridCreatorUIEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridCreator myScriptableObject = (GridCreator)target;

        if (GUILayout.Button("Create"))
        {
          
        }

       
    }
}
