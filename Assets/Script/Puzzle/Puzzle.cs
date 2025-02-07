using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour, IMoveable
{
    [SerializeField] PuzzleData data;
    [SerializeField] Transform rayPoint;

    PuzzleManager manager;

    Vector3 startPos = default;
    [SerializeField] Vector3 startScale = Vector3.one;

    private void Start()
    {
        manager = PuzzleManager.Instance;
        transform.localScale = startScale;

    }

    public void SetStartPos(Vector3 pos) => startPos = pos;

    public bool ControlSucces()
    {
       return GridManager.Instance.ControlPlacement(data);
    }

    public void Move(Vector3 pos)
    {
        transform.localScale = Vector3.one;
        transform.position = pos.Add(x: manager.movePuzzleSelectAddVector.x, y: 8f);
        SetMoveableObject();
    }

    public void MoveEnd(Vector3 pos)
    {
        if (GridManager.Instance.IsPlacement())
        {
            GridManager.Instance.Placement();
            manager.CollectPuzzle(this);
            AudioManager.Instance.PlaySound(AUDIO_TYPE.PLACEMENT_PUZZLE);
            GameManager.Instance.AddMove();
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
