using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public abstract class SliderWatch : MonoBehaviour
{
    protected SliderPiece[] pieces;
    protected int length;
    protected int coef;


    public virtual void Setup()
    {
        coef = 100 / length;
        pieces = new SliderPiece[length];

        for (int i = 0; i < length; i++)
        {
            pieces[i] = transform.Find("Piece_" + i).GetComponent<SliderPiece>();
            pieces[i].nomber = i;
        }
    }



    public void Select(int num)
    {
        num /= coef;

        for (int i = 0; i < length; i++)
        {
            if (i < num)
                pieces[i].Select(true);
            else
                pieces[i].Select(false);
        }
    }
}
