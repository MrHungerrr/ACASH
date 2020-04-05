using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScholarOptions;

public class ScholarEmotions : MonoBehaviour
{
    public GetS.faces face_now { get; private set; }
    private Material face;


    public void Setup()
    {
        face = transform.parent.Find("Head").Find("Model").Find("Face").GetComponent<Renderer>().material;
        Change(GetS.faces.Dead);
    }

    public void Reset()
    {
        Change(GetS.faces.Ussual);
    }



    public void Change(GetS.faces emotion, bool stop_coroutines = true)
    {
        if (stop_coroutines)
            StopAllCoroutines();


        face_now = emotion;
        SetFace(ScholarFaces.get[emotion].texture);
    }


    private void SetFace(Texture t)
    {
        face.SetTexture("_BaseMap", t);
        face.SetTexture("_EmissionMap", t);
    }


    public void Change(GetS.faces emotion, GetS.faces emotion_sec, float time)
    {
        Change(emotion);
        StartCoroutine(ChangeForTime(emotion_sec, time));
    }

    public void Change(GetS.faces emotion_flash, GetS.faces emotion, GetS.faces emotion_sec, float time)
    {
        Change(emotion_flash);
        StartCoroutine(ChangeForTime(emotion, emotion_sec, time));
    }

    public void Change(GetS.faces emotion_flash, GetS.faces emotion)
    {
        Change(emotion_flash);
        StartCoroutine(ChangeForTime(emotion, 1f));
    }



    private IEnumerator ChangeForTime(GetS.faces emotion, float time)
    {
        yield return new WaitForSeconds(time);
        Change(emotion, false);
    }

    private IEnumerator ChangeForTime(GetS.faces emotion, GetS.faces emotion_sec, float time)
    {
        yield return new WaitForSeconds(1f);
        Change(emotion, false);
        yield return new WaitForSeconds(time);
        Change(emotion_sec, false);
    }


}
