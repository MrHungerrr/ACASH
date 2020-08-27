using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct ComputerButtonCollider
{
    public int xPivot => _xPivot;
    public int xCorner => _xCorner;

    public int yPivot => _yPivot;
    public int yCorner => _yCorner;


    [SerializeField] private int _xPivot;
    [SerializeField] private int _xCorner;
    [SerializeField] private int _yPivot;
    [SerializeField] private int _yCorner;

    public ComputerButtonCollider(RectTransform rectTransform)
    {
        rectTransform.anchorMax = new Vector2(0, 1);
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.pivot = new Vector2(0, 1);

        _xPivot = (int)rectTransform.localPosition.x;
        _yPivot = (int)rectTransform.localPosition.y;
        _xCorner = _xPivot + (int)rectTransform.rect.width;
        _yCorner = _yPivot - (int)rectTransform.rect.height;
    }

    public bool IsCollided(in Vector2 Position)
    {
        if (Position.x >= xPivot && Position.x <= xCorner)
            if (Position.y <= yPivot && Position.y >= yCorner)
            {
                return true;
            }

        return false;
    }


    public override string ToString()
    {
        return $"x pivot = {xPivot}  x corner = {xCorner}  y pivot = {yPivot}  y corner = {yCorner}";
    }
}
