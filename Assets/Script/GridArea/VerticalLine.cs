using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VerticalLine : BaseGrid
{
    public override void Hover()
    {
        mainSprite.color = ThemaManager.Instance.cornerHoverColor;
    }

    public override void UnHover()
    {
        mainSprite.color = ThemaManager.Instance.cornerBaseColor;
    }
    public override void Placemnt()
    {
        isFill = true;
        base.Placemnt();
    }

    public override void ClearGrid()
    {
        base.ClearGrid();
        isFill = false;
      //  UnHover();
    }
}
