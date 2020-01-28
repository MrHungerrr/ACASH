using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using N_BH;


public class DeskManager : Singleton<DeskManager>
{
    [HideInInspector]
    public DeskController[] desks;



    public void SetDeskManager()
    {
        desks = GameObject.FindObjectsOfType<DeskController>();
        DeskSort();

        foreach(DeskController dc in desks)
        {
            dc.SetDeskController();
        }
    }



    private void DeskSort()
    {
        for (int i = 0; i < (desks.Length - 1); i++)
            for (int i2 = 0; i2 < (desks.Length - 1 - i); i2++)
            {
                Vector3 pos1 = desks[i2].transform.position;
                Vector3 pos2 = desks[i2 + 1].transform.position;

                if ((pos1.x > pos2.x) || (pos1.x == pos2.x && pos1.z < pos2.z))
                {
                    var buf = desks[i2 + 1];
                    desks[i2 + 1] = desks[i2];
                    desks[i2] = buf;
                }
            }


        for (int i = 0; i < desks.Length; i++)
        {
            desks[i].name = "Desk_" + i;
            //Debug.Log(desks[1, i].position);
        }
    }

}
