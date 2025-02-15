using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Spaces))]
public class SpacesDrawer : PropertyDrawer
{
    public override void OnGUI(Rect a_Rect, SerializedProperty a_Property, GUIContent a_Label)
    {
        float lh = EditorGUIUtility.singleLineHeight;

        Rect labelRect = new Rect(a_Rect.x, a_Rect.y, a_Rect.width - lh * 3f, a_Rect.height);
        EditorGUI.LabelField(labelRect, a_Label);

        float boolStartX = a_Rect.x + labelRect.width;
        Rect boolRect = new Rect(boolStartX, a_Rect.y, lh, lh);

        for (int y = 0; y < 3; y++)
        {
            boolRect.x = boolStartX;
            for (int x = 0; x < 3; x++)
            {
                int currentBool = x + y * 3 + 1;
                EditorGUI.PropertyField(boolRect, a_Property.FindPropertyRelative("space" + currentBool), GUIContent.none);
                boolRect.x += boolRect.height;
            }
            boolRect.y += boolRect.width;
        }
    }

    public override float GetPropertyHeight(SerializedProperty a_Property, GUIContent a_Label)
    {
        return EditorGUIUtility.singleLineHeight * 3f;
    }
}
