using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Searching;
using TMPro;

public class StressWatchScreen : MonoBehaviour
{

    [SerializeField]
    private Scholar scholar;

    [SerializeField]
    private int number;

    private TextMeshPro text;


    public void Setup()
    {
        SIC<TextMeshPro>.Component(this.gameObject, out text);

        try
        {
            if (scholar == null)
                scholar = ScholarManager.get.scholars[number];
        }
        catch
        {

        }
    }

    private void LateUpdate()
    {
        if (scholar != null)
        {
            if (text != null)
                text.text = "Stress\n " + scholar.Stress.value_show.ToString() + "%";
            else
                Setup();
        }
        else
        {
            Setup();
        }
    }





}
