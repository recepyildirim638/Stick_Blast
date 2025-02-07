using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable 
{
    void MoveStart(Vector3 pos);
    void Move(Vector3 pos);
    void MoveEnd(Vector3 pos);
}
