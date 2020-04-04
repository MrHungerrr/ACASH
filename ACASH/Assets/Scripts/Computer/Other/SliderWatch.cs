using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public abstract class SliderWatch : MonoBehaviour
{
    protected SliderPiece[] pieces;
    protected int length;


    public virtual void Setup()
    {
        pieces = new SliderPiece[length];

        for (int i = 0; i < length; i++)
        {
            pieces[i] = transform.Find("Piece_" + i).GetComponent<SliderPiece>();
            pieces[i].nomber = i;
        }
    }



    public void Select(int num)
    {
        int res = (int) (length * (float) (num / 100));

        for (int i = 0; i < length; i++)
        {
            if (i < num)
                pieces[i].Select(true);
            else
                pieces[i].Select(false);
        }
    }
}
