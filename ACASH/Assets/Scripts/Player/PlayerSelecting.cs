using UnityEngine;
using System.Collections;

public class PlayerSelecting
{

    private LayerMask select_layer_mask;
    private LayerMask sight_layer_mask;


    private I_ObjectSelect select;
    private float select_max_range { get; } = 2f;
    public GameObject selected_obj { get; private set; }
    public bool selected { get; private set; }



    public void Update()
    {
        RayCasting();
        CrossHair.get.SelectHair();
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
                Scholar scholar = hits[i].transform.parent.Find("Scholar").GetComponentInChildren<Scholar>();
                scholar.Senses.ISeeYou();
            }
        }
    }



    private void FirstLook(GameObject obj)
    {
        Deselect();

        selected_obj = obj;

        if (selected_obj.TryGetComponent<I_ObjectSelect>(out select))
            CanISelect();
        else
            select = null;
    }

    private void CanISelect()
    {
        if (select.CanISelect())
            Select();
        else
            Deselect();
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
