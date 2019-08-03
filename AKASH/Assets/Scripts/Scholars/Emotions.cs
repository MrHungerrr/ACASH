using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Emotions : MonoBehaviour
{

    private TextMeshPro face;



    void Start()
    {
        face = GetComponent<TextMeshPro>();
    }



    public void ChangeEmotion(string emotion)
    {
        StopAllCoroutines();

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
            case "dead":
                {
                    //Сделать мертвое лицо
                    face.text = "X";
                    break;
                }
        }

    }



    public void ChangeEmotion(string emotion, string emotion_sec, float time)
    {
            ChangeEmotion(emotion);
            StartCoroutine(EmotionForTime(emotion_sec, time));
    }

    public void ChangeEmotion(string emotion_flash, string emotion, string emotion_sec, float time)
    {
            ChangeEmotion(emotion_flash);
            StartCoroutine(EmotionForTime(emotion, emotion_sec, time));
    }

    public void ChangeEmotion(string emotion_flash, string emotion)
    {
            ChangeEmotion(emotion_flash);
            StartCoroutine(EmotionForTime(emotion, 1f));
    }



    private IEnumerator EmotionForTime(string emotion, float time)
    {
        yield return new WaitForSeconds(time);
        ChangeEmotion(emotion);
    }

    private IEnumerator EmotionForTime(string emotion, string emotion_sec, float time)
    {
        yield return new WaitForSeconds(1f);
        ChangeEmotion(emotion);
        yield return new WaitForSeconds(time);
        ChangeEmotion(emotion_sec);
    }


}
