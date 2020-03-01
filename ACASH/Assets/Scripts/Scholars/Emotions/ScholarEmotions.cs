using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarEmotions : MonoBehaviour
{

    private Material face;


    public void Setup()
    {
        face = transform.parent.Find("Head").Find("Model").Find("Face").GetComponent<Renderer>().material;
    }



    public void ChangeEmotion(string emotion)
    {
        StopAllCoroutines();

        switch (emotion)
        {
            case "ussual":
                {
                    //Сделать обычное лицо
                    SetFace(ScholarManager.get.ussual);
                    break;
                }
            case "smile":
                {
                    //Улыбнуться
                    SetFace(ScholarManager.get.smile);
                    break;
                }
            case "happy":
                {
                    //Сделать счастливое лицо
                    SetFace(ScholarManager.get.happy);
                    break;
                }
            case "upset":
                {
                    //Расстроиться/Обидеться
                    SetFace(ScholarManager.get.upset);
                    break;
                }
            case "sad":
                {
                    //Сделать печальное лицо
                    SetFace(ScholarManager.get.sad);
                    break;
                }
            case "suprised":
                {
                    //Сделать удивленное лицо
                    SetFace(ScholarManager.get.suprised);
                    break;
                }
            case "ask":
                {
                    //Сделать вопросительное лицо
                    SetFace(ScholarManager.get.ask);
                    break;
                }
            case "dead":
                {
                    //Сделать мертвое лицо
                    SetFace(ScholarManager.get.dead);
                    break;
                }
        }

    }


    private void SetFace(Texture t)
    {
        face.SetTexture("_BaseMap", t);
        face.SetTexture("_EmissionMap", t);
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
