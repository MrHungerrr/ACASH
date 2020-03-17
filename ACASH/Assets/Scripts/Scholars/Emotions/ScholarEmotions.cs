using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarEmotions : MonoBehaviour
{

    private Material face;


    public void Setup()
    {
        face = transform.parent.Find("Head").Find("Model").Find("Face").GetComponent<Renderer>().material;
        Change("dead");
    }

    public void Reset()
    {
        Change("ussual");
    }



    public void Change(string emotion)
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


    public void Change(string emotion, string emotion_sec, float time)
    {
            Change(emotion);
            StartCoroutine(ChangeForTime(emotion_sec, time));
    }

    public void Change(string emotion_flash, string emotion, string emotion_sec, float time)
    {
            Change(emotion_flash);
            StartCoroutine(ChangeForTime(emotion, emotion_sec, time));
    }

    public void Change(string emotion_flash, string emotion)
    {
            Change(emotion_flash);
            StartCoroutine(ChangeForTime(emotion, 1f));
    }



    private IEnumerator ChangeForTime(string emotion, float time)
    {
        yield return new WaitForSeconds(time);
        Change(emotion);
    }

    private IEnumerator ChangeForTime(string emotion, string emotion_sec, float time)
    {
        yield return new WaitForSeconds(1f);
        Change(emotion);
        yield return new WaitForSeconds(time);
        Change(emotion_sec);
    }


}
