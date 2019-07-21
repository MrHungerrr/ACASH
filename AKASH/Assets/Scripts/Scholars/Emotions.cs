using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Emotions : MonoBehaviour
{

    private TextMeshPro face;
    private bool canChange = true;

    // Start is called before the first frame update
    void Start()
    {
        face = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChageEmotion(string emotion)
    {
        if (canChange)
        {
            switch (emotion)
            {
                case "sad":
                    {
                        //Сделать печальное лицо
                        face.text = ":(";
                        break;
                    }
                case "happy":
                    {
                        //Сделать счастливое лицо
                        face.text = ":)";
                        break;
                    }
                case "suprised":
                    {
                        //Сделать удивленное лицо
                        face.text = ":O";
                        break;
                    }
                case "ussual":
                    {
                        //Сделать обычное лицо
                        face.text = ":|";
                        break;
                    }
            }
        }
    }

    public void ChageEmotion(string emotion, float time)
    {
        if (canChange)
        {
            switch (emotion)
            {
                case "sad":
                    {
                        //Сделать печальное лицо
                        face.text = ":(";
                        break;
                    }
                case "happy":
                    {
                        //Сделать счастливое лицо
                        face.text = ":)";
                        break;
                    }
                case "suprised":
                    {
                        //Сделать удивленное лицо
                        face.text = ":O";
                        break;
                    }
                case "ussual":
                    {
                        //Сделать обычное лицо
                        face.text = ":|";
                        break;
                    }
            }
            StartCoroutine(EmotionForTime(time));
        }
    }

    private IEnumerator EmotionForTime(float time)
    {
        canChange = false;
        yield return new WaitForSeconds(time);
        canChange = true;
        ChageEmotion("ussual");
    }


}
