using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccsessManager : MonoSingleton<AccsessManager>
{
    [SerializeField] public CameraManager cameraManager;
    [SerializeField] public GameManager gameManager;
    [SerializeField] public GridManager gridManager;
    [SerializeField] public DataManager dataManager;

}
