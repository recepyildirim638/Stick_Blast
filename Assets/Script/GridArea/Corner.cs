using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Corner : BaseGrid, ISelectiable
{
  
    public void OnMove(PuzzleData puzzleData)
    {
        GridManager.Instance.HoverPuzzle(puzzleData, this);
    }

    public override void Hover()
    {
        
        mainSprite.color = ThemaManager.Instance.cornerHoverColor;
    }

    public override void UnHover()
    {
        if (isFill)
            mainSprite.color = ThemaManager.Instance.cornerAddedColor;
        else
            mainSprite.color = ThemaManager.Instance.cornerBaseColor;
    }

    public override void Placemnt()
    {
        isFill = true;
        base.Placemnt();
        ParticleManager.ins.SetParticle(PoolItems.CORNER_PLACEMENT, transform.position);
    }

    public override void ClearGrid()
    {
        base.ClearGrid();
        isFill = false;
      
        //  UnHover();
    }

   
}
