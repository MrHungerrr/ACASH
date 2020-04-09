using System;
using UnityEngine;
using System.Collections;

public class PlayerSelecting
{
    public Reasons active { get;}

    private LayerMask select_layer_mask;
    private LayerMask sight_layer_mask;
    private float select_max_range { get; } = 2f;


    public bool selected { get; private set; }
    private I_ObjectSelect select;
    public GameObject selected_obj { get; private set; }


    public PlayerSelecting()
    {
        active = new Reasons();
        Deselect();

        select_layer_mask = LayerMask.GetMask("Selectable", "Default");
        sight_layer_mask = LayerMask.GetMask("Sight Layer");
    }



    public void Update()
    {

        RayCasting();
        CrossHair.get.SelectHair();

        LookingAtScholars();
    }


    public void Enable(Type reason)
    {
        active.Remove(reason);
    }

    public void Disable(Type reason)
    {
        active.Add(reason);
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





    }

    private void LookingAtScholars()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, 20, sight_layer_mask);

        for (int i = 0; i < hits.Length; i++)
        {

            if (hits[i].transform != null)
            {
                Scholar scholar = hits[i].collider.transform.parent.GetComponent<Scholar>();

                scholar.Senses.TeacherLookAtUs();
            }
        }
    }


    public bool TryGetScholar()
    {
        if (selected)
        {
            Scholar scholar;

            if (selected_obj.TryGetComponent<Scholar>(out scholar))
            {
                if (scholar.active)
                {
                    Player.get.Talk.SetScholar(scholar);
                    return true;
                }
            }
        }

        Player.get.Talk.ResetScholar();
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
