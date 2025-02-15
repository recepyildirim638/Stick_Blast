using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
   public static Action GameEndFail { get; set; }

    public static Action GameEndWin { get; set; }
    public static Action<int> ClearGrid { get;  set; }


    public static Action<Vector3> CollectDiamaont { get; set; }
    public static Action<Vector3> CollectStar { get; set; }
    public static Action ChangeSound { get;  set; }
    
    
    public static Action GridAreaReady { get; set; }
    public static Action ResetGameArea { get; set; }
    public static Action PlacementPuzzle { get;  set; }
}
