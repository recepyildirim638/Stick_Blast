using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MyLevelData))]
public class LevelDatasDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty sizeProp = property.FindPropertyRelative("size");
        SerializedProperty spacesProp = property.FindPropertyRelative("spaces");

        EditorGUI.BeginProperty(position, label, property);

        // Boyut Ayarlarý
        Rect sizeRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(sizeRect, sizeProp, new GUIContent("Size"));

        int width = sizeProp.vector2IntValue.x;
        int height = sizeProp.vector2IntValue.y;

        // Dizi Boyutunu Güncelle
        if (spacesProp.arraySize != width * height)
            spacesProp.arraySize = width * height;

        float cellSize = 20;
        float startX = position.x;
        float startY = position.y + EditorGUIUtility.singleLineHeight + 25;

        // Izgara Çizimi
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = x + y * width;
                Rect cellRect = new Rect(startX + x * cellSize, startY + y * cellSize, cellSize, cellSize);
                spacesProp.GetArrayElementAtIndex(index).boolValue = EditorGUI.Toggle(cellRect, spacesProp.GetArrayElementAtIndex(index).boolValue);
            }
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty sizeProp = property.FindPropertyRelative("size");
        int height = sizeProp.vector2IntValue.y;
        return EditorGUIUtility.singleLineHeight * 2 + height * 20 + 10;
    }
}
