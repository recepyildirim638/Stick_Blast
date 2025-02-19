using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PuzzleManager : MonoSingleton<PuzzleManager> 
{

    [SerializeField] public Vector2 movePuzzleSelectAddVector = new();
    [HideInInspector] public ISelectiable lastSelectiable;

    [SerializeField] public LayerMask mask;
    [SerializeField] GameObject []puzzlesPrefabs;
    [SerializeField] Transform[] createPos;

    List<Puzzle> createdPuzle = new List<Puzzle>();

    int added = 0;

    private void Start()
    {
        puzzleCalculteList = new int[puzzlesPrefabs.Length];
    }

    private void OnEnable()
    {
        ActionManager.GridAreaReady += GridAreaReadyFunc;
        ActionManager.ResetGameArea += ResetAll;

    }
    private void OnDisable()
    {
        ActionManager.GridAreaReady -= GridAreaReadyFunc;
        ActionManager.ResetGameArea -= ResetAll;

    }

    private void GridAreaReadyFunc()
    {
        SetPosition();
        PreCalculetCount();
        CreatePuzzle();
    }
    void SetPosition()
    {
        Camera cam = Camera.main;
        float height = cam.orthographicSize * 2f;
        Vector3 camPos = cam.transform.position;

        float bottomPos = (camPos.y - height / 2) + 6f;
        transform.position = new Vector3(0f, bottomPos, 0f);    

      
    }

    public void CollectPuzzle(Puzzle puzzle)
    {
        createdPuzle.Remove(puzzle);

        added++;

        if (added == 3)
        {
            PreCalculetCount();
            CreatePuzzle();
        }

        ControlPlacement();
    }

    [SerializeField] int[] puzzleCalculteList;
    [SerializeField] List<int> calculatedList = new List<int>();
    private void PreCalculetCount()
    {
        for (int i = 0; i <  puzzleCalculteList.Length; i++)
            puzzleCalculteList[i] = 0;

        for (int i = 0; i < puzzlesPrefabs.Length; i++)
        {
            puzzleCalculteList[i] = GridManager.Instance.PreControlPlacementCount(puzzlesPrefabs[i].GetComponent<Puzzle>().GetData());
        }

        calculatedList.Clear();
        calculatedList.AddRange(puzzleCalculteList
            .Select((value, index) => new { value, index }) 
            .Where(item => item.value > 2)
            .Select(item => item.index));

        if (!calculatedList.Any())
        {
            calculatedList.AddRange(puzzleCalculteList
           .Select((value, index) => new { value, index })
           .Where(item => item.value > 1)
           .Select(item => item.index));
        }

        if (!calculatedList.Any())
        {
            calculatedList.AddRange(puzzleCalculteList
           .Select((value, index) => new { value, index })
           .Where(item => item.value >= 0)
           .Select(item => item.index));
        }
    }


    public void CreatePuzzle()
    {
        createdPuzle.Clear();
        added = 0;
        for (int i = 0; i < createPos.Length; i++)
        {
            //  int rnd = Random.Range(0, puzzlesPrefabs.Length);
            int rnd = Random.Range(0, calculatedList.Count);
            rnd = calculatedList[rnd];
            GameObject create = Instantiate(puzzlesPrefabs[rnd]);
            create.GetComponent<Puzzle>().SetStartPos(createPos[i].position);
            
            MoveScane(create, i);
            create.transform.position = createPos[i].position;

            createdPuzle.Add(create.GetComponent<Puzzle>());
        }
        DOVirtual.DelayedCall(0.4f, () => {
            GameAudioManager.Instance.PlaySound(AUDIO_TYPE.CREATED_PUZZLE);
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
