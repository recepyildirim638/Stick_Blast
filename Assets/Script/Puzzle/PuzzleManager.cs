using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoSingleton<PuzzleManager> 
{

    [HideInInspector] public Vector2 movePuzzleSelectAddVector = new();
    [HideInInspector] public ISelectiable lastSelectiable;

    [SerializeField] public LayerMask mask;
    [SerializeField] GameObject []puzzlesPrefabs;
    [SerializeField] Transform[] createPos;

    List<Puzzle> createdPuzle = new List<Puzzle>();

    int added = 0;
   

    public void CollectPuzzle(Puzzle puzzle)
    {
        createdPuzle.Remove(puzzle);

        added++;

        if (added == 3)
            CreatePuzzle();

        ControlPlacement();
    }

    public void CreatePuzzle()
    {
        createdPuzle.Clear();
        added = 0;
        for (int i = 0; i < createPos.Length; i++)
        {
            int rnd = Random.Range(0, puzzlesPrefabs.Length);
            GameObject create = Instantiate(puzzlesPrefabs[rnd]);
            create.GetComponent<Puzzle>().SetStartPos(createPos[i].position);
            
            MoveScane(create, i);
            create.transform.position = createPos[i].position;

            createdPuzle.Add(create.GetComponent<Puzzle>());
        }
        DOVirtual.DelayedCall(0.4f, () => {
            AudioManager.Instance.PlaySound(AUDIO_TYPE.CREATED_PUZZLE);
        });
       
    }

    private void MoveScane(GameObject obj, int index)
    {
        obj.transform
            .DOMove(createPos[index].position, 0.6f)
            .SetEase(Ease.InBack)
            .From(createPos[index].position.With(x: 49f));
    }

    void ControlPlacement()
    {
        for (int i = 0; i < createdPuzle.Count; i++)
        {
            if (createdPuzle[i].ControlSucces())
                return;
        }

        GameManager.Instance.GameFail();
        ActionManager.GameEndFail?.Invoke();

       
     
    }

    public void ResetAll()
    {
        for (int i = 0; i < createdPuzle.Count; i++)
            Destroy(createdPuzle[i].gameObject);

        createdPuzle.Clear();
        added = 0;
    }
}
