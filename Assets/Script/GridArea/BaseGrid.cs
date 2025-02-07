using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGrid : MonoBehaviour
{
    [SerializeField] protected Vector2Byte point;
    public Vector2Byte GetPoint() => point;

    [SerializeField] protected SpriteRenderer mainSprite;

    [SerializeField] protected bool isFill;

    public bool GetFill() => isFill;

  
    public abstract void Hover();

    public abstract void UnHover();

    public virtual void Placemnt()
    {
        mainSprite.DOColor(ThemaManager.Instance.cornerAddedColor, .4f).From(Color.white);
        transform.DOScale(1.1f, 0.2f).SetLoops(2, LoopType.Yoyo);
    }

    public virtual void ClearGrid()
    {
        mainSprite.DOKill();
        transform.DOKill(true);

        mainSprite.DOColor(Color.white, 0.2f).OnComplete(() =>
        {
            UnHover();
        });
        
    }
}
