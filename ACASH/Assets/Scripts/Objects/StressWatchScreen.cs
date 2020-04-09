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


    void Start()
    {
        SIC<TextMeshPro>.Component(this.gameObject, out text);

        if (scholar == null)
            scholar = ScholarManager.get.scholars[number];
    }



    private void Update()
    {
        text.text = "Stress:" + scholar.Stress.value_show.ToString() + "%";
    }


}
