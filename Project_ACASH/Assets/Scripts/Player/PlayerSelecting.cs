using System;
using UnityEngine;
using System.Collections;

public class PlayerSelecting
{

    private const float SELECT_MAX_RANGE = 2f;
    private readonly LayerMask SELECT_LAYER_MASK;


    public bool SelectingIsActive { get; private set; }
    public GameObject SelectedObject { get; private set; }


    private IObjectSelect _select;
    private readonly Reason _reasonsToBeDisabled;



    public PlayerSelecting()
    {
        _reasonsToBeDisabled = new Reason();
        Deselect();

        SELECT_LAYER_MASK = LayerMask.GetMask("Selectable", "Default");
    }



    public void Update()
    {
        RayCasting();
        CrossHair.Instance.SelectHair();
    }


    public void Enable(Type reason)
    {
        _reasonsToBeDisabled.Remove(reason);
    }

    public void Disable(Type reason)
    {
        _reasonsToBeDisabled.Add(reason);
    }


    private void RayCasting()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;


        //Рейкаст интерактивных объектов
        if (Physics.Raycast(ray, out hit, SELECT_MAX_RANGE, SELECT_LAYER_MASK))
        {
            if (SelectedObject != hit.collider.gameObject)
            {
                FirstLook(hit.collider.gameObject);
            }
            else if (_select != null)
            {
                CanISelect();
            }
        }
        else if (SelectingIsActive && _select != null)
        {
            Deselect();
        }
    }



    private void FirstLook(GameObject obj)
    {
        Deselect();

        if (obj.layer == 9)
        {
            SelectedObject = obj;
     
            if (SelectedObject.TryGetComponent(out _select))
            {
                CanISelect();
            }
            else
                _select = null;
        }
        else
        {
            SelectedObject = null;
            _select = null;
        }
    }

    private void CanISelect()
    {
        if (_reasonsToBeDisabled.GiveMeChance && _select.CanISelect())
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
        if (!SelectingIsActive)
        {
            _select.Select();
            SelectingIsActive = true;
        }
    }

    private void Deselect()
    {
        if (SelectingIsActive)
        {
            if (_select != null)
                _select.Deselect();
            SelectingIsActive = false;
        }
    }
}
