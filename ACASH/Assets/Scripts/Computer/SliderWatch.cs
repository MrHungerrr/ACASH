using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class SliderWatch : MonoBehaviour
{
    private SliderPiece[] pieces = new SliderPiece[10];

    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            pieces[i] = transform.Find("Piece_" + i).GetComponent<SliderPiece>();
            pieces[i].nomber = i;
        }
    }



    public void Select(int num)
    {
        Debug.Log(num);

        for (int i = 0; i < 10; i++)
        {
            if (i < num)
                pieces[i].Select(true);
            else
                pieces[i].Select(false);
        }
    }
}
