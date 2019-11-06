using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using N_BH;


public class StudentStressAgent : MonoBehaviour
{
    private StressCell[] cells;

    public void Set()
    {
        int buf;
        GameObject[] ss = new GameObject[]
        {
            GameObject.Find("Stress 2x2"),
            GameObject.Find("Stress 3x2"),
            GameObject.Find("Stress 3x3"),
            GameObject.Find("Stress 3x4")
        };


        switch (ScholarManager.get.scholars.Length)
        {
            case 4:
                {
                    buf = 0;
                    cells = new StressCell[4];
                    break;
                }
            case 6:
                {
                    buf = 1;
                    cells = new StressCell[6];
                    break;
                }
            case 9:
                {
                    buf = 2;
                    cells = new StressCell[9];
                    break;
                }
            case 12:
                {
                    buf = 3;
                    cells = new StressCell[12];
                    break;
                }
            default:
                {
                    buf = 3;
                    cells = new StressCell[12];
                    break;
                }
        }

        for(int i = 0; i < ss.Length; i++)
        {
            if (i == buf)
                ss[i].SetActive(true);
            else
                ss[i].SetActive(false);
        }


        for (int i = 0; i<cells.Length; i++)
        {
            Debug.Log(buf + "_Cell_" + i);
            cells[i] = GameObject.Find(buf + "_Cell_" + i).GetComponent<StressCell>();
            Debug.Log("Название ячейки - " + cells[i].name);

            try
            {
                cells[i].Set(ScholarManager.get.scholars[i]);
            }
            catch
            {
                cells[i].gameObject.SetActive(false);
                Debug.Log("Недостаточно учеников для клетки - " + i);
            }
        }
    }



    public void Refresh()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].Refresh();
        }
    }
}
