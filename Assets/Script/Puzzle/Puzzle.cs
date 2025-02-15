using System;
using UnityEngine;

[Serializable]
public struct Spaces
{
    public bool space1;
    public bool space2;
    public bool space3;
    public bool space4;
    public bool space5;
    public bool space6;
    public bool space7;
    public bool space8;
    public bool space9;
}


public class Puzzle : MonoBehaviour, IMoveable
{
    [SerializeField] MyLevelData levelData;
   

    [SerializeField] PuzzleData data;
    [SerializeField] Transform rayPoint;

    PuzzleManager manager;

    Vector3 startPos = default;
    Camera cam;

    [SerializeField] 
    Vector3 startScale = Vector3.one;

    [ContextMenu("test")]
    public void Test()
    {
        for (int i = 0; i < levelData.size.y; i++)
        {
            bool result = true;
            for(int j = 0; j < levelData.size.x; j++)
            {
                if (!levelData.GetSpace(j, i))
                {
                   result = false;
                }
            }
            
            if(result)
            {
                Debug.Log(i);
            }
               
          
        }
    }

    private void Start()
    {
        cam = Camera.main;
        manager = PuzzleManager.Instance;
        transform.localScale = startScale;

    }

    public PuzzleData GetData() { return data; }

    public void SetStartPos(Vector3 pos) => startPos = pos;

    public bool ControlSucces()
    {
       return GridManager.Instance.ControlPlacement(data);
    }

   
    private Vector2 GetScreenToWorldPosition()
    {
        Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(worldPos.x, worldPos.y);
    }
    private void OnMouseEnter()
    {
        Vector2 worldPos = GetScreenToWorldPosition();
        MoveStart(worldPos);
    }
    private void OnMouseDrag()
    {
        Vector2 worldPos = GetScreenToWorldPosition();
        Move(worldPos);
    }
    private void OnMouseUp()
    {
        Vector2 worldPos = GetScreenToWorldPosition();
        MoveEnd(worldPos);
    }

    public void Move(Vector3 pos)
    {
        transform.localScale = Vector3.one;
        transform.position = pos.Add(y: 8f);
        SetMoveableObject();
    }

    public void MoveEnd(Vector3 pos)
    {
        if (GridManager.Instance.IsPlacement())
        {
            GridManager.Instance.Placement();
            manager.CollectPuzzle(this);
            GameAudioManager.Instance.PlaySound(AUDIO_TYPE.PLACEMENT_PUZZLE);
            ActionManager.PlacementPuzzle?.Invoke();
            Destroy(gameObject);
        }
        else
        {
            transform.position = startPos;
            transform.localScale = startScale;
        }
    }

    public void MoveStart(Vector3 pos)
    {
        manager.movePuzzleSelectAddVector.x = transform.position.x - pos.x;
    }


    private void SetMoveableObject()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, Vector2.zero, 10f, manager.mask);

        if (hit.collider != null)
        {
            ISelectiable selectiable = hit.collider.gameObject.GetComponent<ISelectiable>();

            if (selectiable == null)
            {
                GridManager.Instance.UnHoverPuzzle();
                return;
            }


            if (manager.lastSelectiable == null)
            {
                selectiable?.OnMove(data);
                manager.lastSelectiable = selectiable;
                return;
            }

            if (!ReferenceEquals(selectiable, manager.lastSelectiable))
            {
                selectiable?.OnMove(data);
                manager.lastSelectiable = selectiable;
            }
        }
    }
}
