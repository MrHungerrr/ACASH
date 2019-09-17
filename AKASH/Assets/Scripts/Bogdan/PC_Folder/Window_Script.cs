using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Window_Script : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private Vector3 startPosition;
    private Vector3 offset;
    private float minDist = 30;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        offset = Input.mousePosition - startPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition - offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Vector3.Distance(startPosition, transform.position) < minDist)
        {
            transform.position = startPosition;
        }
    }

    private void OnEnable()
    {

        if (this.gameObject.name == "Tension_window")
        {

<<<<<<< HEAD
=======
            for (int i = 0; i < sc; i++)
            {
                GameObject gm;
                gm = Instantiate<GameObject>(Resources.Load<GameObject>("PC_Prefabs/Tension_Slider"), this.gameObject.transform.GetChild(0).transform);
                gm.GetComponent<Slider>().value = ch.student[i].stress;
                gm.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().SetText(ch.student[i].gameObject.name);

            }
        }
    }
>>>>>>> parent of 123f522... stresscheking 1.35

            for (int i = 0; i < GameObject.FindObjectOfType<CheatHelper>().studentcount; i++)
            {
                Instantiate<GameObject>(Resources.Load<GameObject>("PC_Prefabs/Tension_Slider"), this.gameObject.transform);
            }
        }
    }
}
