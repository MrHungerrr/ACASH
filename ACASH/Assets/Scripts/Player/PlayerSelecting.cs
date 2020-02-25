using UnityEngine;
using System.Collections;

public class PlayerSelecting
{


    private LayerMask select_layer_mask = LayerMask.GetMask("Selectable", "Default");
    private LayerMask sight_layer_mask = LayerMask.GetMask("Sight Layer");
    private float select_max_range { get; } = 2f;


    public bool selected { get; private set; }
    private I_ObjectSelect select;
    public GameObject selected_obj { get; private set; }
    public Scholar selected_scholar;






    public void Update()
    {
        if (!Player.get.Camera.zoom)
        {
            RayCasting();
            CrossHair.get.SelectHair();
        }
    }


    private void RayCasting()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;


        //Рейкаст интерактивных объектов
        if (Physics.Raycast(ray, out hit, select_max_range, select_layer_mask))
        {
            if (selected_obj != hit.collider.gameObject)
            {
                FirstLook(hit.collider.gameObject);
            }
            else if (select != null)
            {
                CanISelect();
            }
        }
        else if (selected && select != null)
        {
            Deselect();
        }




        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, 20, sight_layer_mask);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform != null)
            {
                Scholar scholar = hits[i].transform.parent.Find("Body").GetComponentInChildren<Scholar>();
                scholar.Senses.ISeeYou();
            }
        }
    }


    public bool TryGetScholar()
    {
        if (selected_obj != null)
        {
            if (selected_obj.TryGetComponent<Scholar>(out selected_scholar))
            {
                if (!selected_scholar.Execute.executed)
                {
                    Player.get.Talk.scholar = selected_scholar;
                    return true;
                }
            }
        }
        return false;
    }



    private void FirstLook(GameObject obj)
    {
        Deselect();

        if (obj.layer == 9)
        {
            selected_obj = obj;
     
            if (selected_obj.TryGetComponent(out select))
            {
                CanISelect();
            }
            else
                select = null;
        }
        else
        {
            selected_obj = null;
            select = null;
        }
    }

    private void CanISelect()
    {
        if (select.CanISelect())
        {
            Select();
        }
        else
        {
            Deselect();
        }
    }

    private void Select()
    {
        if (!selected)
        {
            select.Select();
            selected = true;
        }
    }

    private void Deselect()
    {
        if (selected)
        {
            if (select != null)
                select.Deselect();
            selected = false;
        }
    }


}
