using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Single;


public class StudentStress : MonoBehaviour
{
    private StressCell[] cells;
    private int number;

    public void Setup()
    {
        Transform transform_ss = transform.Find("Student Stress");

        GameObject[] ss = new GameObject[]
        {
            transform_ss.Find("Stress 2x2").gameObject,
            transform_ss.Find("Stress 3x2").gameObject,
            transform_ss.Find("Stress 3x3").gameObject,
        };


        switch (ScholarManager.Instance.Scholars.Length)
        {
            case 4:
                {
                    number = 0;
                    cells = new StressCell[4];
                    break;
                }
            case 6:
                {
                    number = 1;
                    cells = new StressCell[6];
                    break;
                }
            case 9:
                {
                    number = 2;
                    cells = new StressCell[9];
                    break;
                }
            default:
                {
                    number = 2;
                    cells = new StressCell[9];
                    break;
                }
        }

        for(int i = 0; i < ss.Length; i++)
        {
            if (i == number)
            {
                ss[i].SetActive(true);
                transform_ss = ss[i].transform;
            }
            else
                ss[i].SetActive(false);
        }

        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = transform_ss.Find("Cell_" + i).GetComponent<StressCell>();
        }

        SetScholars();

        InvokeRepeating("Refresh", 0f, 1f);
    }


    public void SetScholars()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            try
            {
                cells[i].Set(ScholarManager.Instance.Scholars[i]);
            }
            catch
            {
                cells[i].gameObject.SetActive(false);
                //Debug.Log("Недостаточно учеников для клетки - " + i);
            }
        }
    }




    public void Refresh()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            if(cells[i].gameObject.activeSelf)
                cells[i].Refresh();
        }
    }
}
