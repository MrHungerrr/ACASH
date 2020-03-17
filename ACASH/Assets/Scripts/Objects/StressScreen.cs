using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StressScreen : MonoBehaviour
{
    private TextMeshPro Text;
    public Scholar Scholar;
    private float timer;
    private const float timer_const = 1f;

    private void Awake()
    {
        Text = transform.GetComponentInChildren<TextMeshPro>();
    }


    private void Update()
    {
        if (timer < 0f)
        {
            Text.text = "Stress: " + Scholar.Stress.value;
            timer = timer_const;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
