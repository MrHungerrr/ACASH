using System;
using System.Collections.Generic;
using Places;
using UnityEngine;
using Searching;

[RequireComponent(typeof(PlaceAgent))]
public class ClassAgent: MonoBehaviour
#region IInitialization
#if UNITY_EDITOR
    ,IInitialization
#endif
#endregion
{

    #region Initializator
#if UNITY_EDITOR

    public bool AutoInitializate => true;

    private enum FillType
    {
        Hierarchy,
        Manual
    }

    [SerializeField]
    private FillType fillType;


    public void Initializate()
    {
        switch (fillType)
        {
            case FillType.Hierarchy:
                {
                    _placeAgent = GetComponent<PlaceAgent>();
                    _placeAgent.Initializate();

                    _scholars = SIC<Scholar>.ComponentsDown(this.transform);

                    break;
                }
            case FillType.Manual:
                {

                    break;
                }
        }

        //if (_scholars.Length != _placeAgent.Places[PlaceManager.place.Desk].Length)
        //    throw new Exception($"Количество учеников - {_scholars.Length}, Количество парт - {_placeAgent.Places[PlaceManager.place.Desk].Length}");

        if (_scholars.Length != _placeAgent.Places[PlaceManager.place.DockStation].Length)
            throw new Exception($"Количество учеников - {_scholars.Length}, Количество Док-станций - {_placeAgent.Places[PlaceManager.place.DockStation].Length}");

        ScholarInitialization();
    }


    private void ScholarInitialization()
    {
        ScholarMove scholarMove;

        for (int i = 0; i < _scholars.Length; i++)
        {
            scholarMove = _scholars[i].GetComponent<ScholarMove>();
            scholarMove.Position(_placeAgent.Places[PlaceManager.place.DockStation][i].Destination);
            scholarMove.Rotation(_placeAgent.Places[PlaceManager.place.DockStation][i].SightGoal);
        }
    }

#endif
    #endregion


    public PlaceAgent PlaceAgent => _placeAgent;


    [SerializeField] private Scholar[] _scholars;

    [SerializeField] private PlaceAgent _placeAgent;



    public void SetLevel()
    {
        ScholarSetup();
    }





    private void ScholarSetup()
    {
        for (int i = 0; i < _scholars.Length; i++)
        {
            _scholars[i].Setup(this);
        }
    }



}

