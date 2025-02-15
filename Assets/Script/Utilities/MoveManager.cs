using UnityEngine;

public class MoveManager : MonoBehaviour
{
    [SerializeField] private Camera _cam;

    private IMoveable moveObject;
    private bool isLoaded = false;

    public void Initialize()
    {
        isLoaded = true;
    }

    private Vector2 GetScreenToWorldPosition()
    {
        Vector3 worldPos = _cam.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(worldPos.x, worldPos.y);
    }

    private void SetMoveableObject()
    {
        Vector2 worldPos = GetScreenToWorldPosition();
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null)
        {
            moveObject = hit.collider.GetComponent<IMoveable>();
        }
    }

    private void Update()
    {
       

        if (Input.GetMouseButtonDown(0))
        {
            SetMoveableObject();

            if (moveObject != null)
                moveObject.MoveStart(GetScreenToWorldPosition());
        }

        if (moveObject == null)
            return;

        if (Input.GetMouseButton(0))
        {
            MoveToMoveableObject();
        }

        if (Input.GetMouseButtonUp(0))
        {
            moveObject.MoveEnd(GetScreenToWorldPosition());
            moveObject = null;
        }
    }

    private void MoveToMoveableObject()
    {
        Vector2 worldPos = GetScreenToWorldPosition();
        moveObject.Move(worldPos);
    }
}
