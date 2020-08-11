using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScholarEmotions : MonoBehaviour
{
    public ScholarFaces.Types FaceType { get; private set; }

    private Material _face;


    public void Setup()
    {
        _face = transform.parent.Find("Head").Find("Model").Find("Face").GetComponent<Renderer>().material;
        Change(ScholarFaces.Types.Dead);
    }

    public void Reset()
    {
        Change(ScholarFaces.Types.Ussual);
    }



    public void Change(ScholarFaces.Types emotion, bool stop_coroutines = true)
    {
        if (stop_coroutines)
            StopAllCoroutines();


        FaceType = emotion;
        SetFace(ScholarFaces.Get[emotion].texture);
    }


    private void SetFace(Texture t)
    {
        _face.SetTexture("_BaseMap", t);
        _face.SetTexture("_EmissionMap", t);
    }


    public void Change(ScholarFaces.Types emotion, ScholarFaces.Types emotion_sec, float time)
    {
        Change(emotion);
        StartCoroutine(ChangeForTime(emotion_sec, time));
    }

    public void Change(ScholarFaces.Types emotion_flash, ScholarFaces.Types emotion, ScholarFaces.Types emotion_sec, float time)
    {
        Change(emotion_flash);
        StartCoroutine(ChangeForTime(emotion, emotion_sec, time));
    }

    public void Change(ScholarFaces.Types emotion_flash, ScholarFaces.Types emotion)
    {
        Change(emotion_flash);
        StartCoroutine(ChangeForTime(emotion, 1f));
    }



    private IEnumerator ChangeForTime(ScholarFaces.Types emotion, float time)
    {
        yield return new WaitForSeconds(time);
        Change(emotion, false);
    }

    private IEnumerator ChangeForTime(ScholarFaces.Types emotion, ScholarFaces.Types emotion_sec, float time)
    {
        yield return new WaitForSeconds(1f);
        Change(emotion, false);
        yield return new WaitForSeconds(time);
        Change(emotion_sec, false);
    }


}
