using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Emotions : MonoBehaviour
{

    private TextMeshPro face;
    private bool canChange = true;



    void Start()
    {
        face = GetComponent<TextMeshPro>();
    }



    public void ChangeEmotion(string emotion)
    {
        if (canChange)
        {
            switch (emotion)
            {
                case "ussual":
                    {
                        //Сделать обычное лицо
                        face.text = ":|";
                        break;
                    }
                case "upset":
                    {
                        //Расстроиться/Обидеться
                        face.text = ":(";
                        break;
                    }
                case "sad":
                    {
                        //Сделать печальное лицо
                        face.text = ":[";
                        break;
                    }
                case "smile":
                    {
                        //Улыбнуться
                        face.text = ":)";
                        break;
                    }
                case "happy":
                    {
                        //Сделать счастливое лицо
                        face.text = ":]";
                        break;
                    }
                case "suprised":
                    {
                        //Сделать удивленное лицо
                        face.text = ":O";
                        break;
                    }
            }
        }
    }



    public void ChangeEmotion(string emotion, string emotion_sec, float time)
    {
        if (canChange)
        {
            ChangeEmotion(emotion);
            StopAllCoroutines();
            StartCoroutine(EmotionForTime(emotion_sec, time));
        }
    }

    public void ChangeEmotion(string emotion_flash, string emotion, string emotion_sec, float time)
    {
        if (canChange)
        {
            ChangeEmotion(emotion_flash);
            StopAllCoroutines();
            StartCoroutine(EmotionForTime(emotion, emotion_sec, time));
        }
    }

    public void ChangeEmotion(string emotion_flash, string emotion)
    {
        if (canChange)
        {
            ChangeEmotion(emotion_flash);
            StopAllCoroutines();
            StartCoroutine(EmotionForTime(emotion, 1f));
        }
    }



    private IEnumerator EmotionForTime(string emotion, float time)
    {
        canChange = false;
        yield return new WaitForSeconds(time);
        canChange = true;
        ChangeEmotion(emotion);
    }

    private IEnumerator EmotionForTime(string emotion, string emotion_sec, float time)
    {
        canChange = false;
        yield return new WaitForSeconds(1f);
        canChange = true;
        ChangeEmotion(emotion);
        canChange = false;
        yield return new WaitForSeconds(time);
        canChange = true;
        ChangeEmotion(emotion_sec);
    }


}
