using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct MyLevelData 
{
    public Vector2Int size;
    public bool[] spaces;

    public MyLevelData(Vector2Int size)
    {
        this.size = size;
        spaces = new bool[size.x * size.y];
    }

    public bool GetSpace(int x, int y) => spaces[x + y * size.x];
    public void SetSpace(int x, int y, bool value) => spaces[x + y * size.x] = value;
}
