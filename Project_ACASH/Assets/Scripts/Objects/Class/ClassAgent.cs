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

    public PlaceAgent PlaceAgent => _placeAgent;


    [SerializeField]
    private Scholar[] _scholars;

    [SerializeField]
    private PlaceAgent _placeAgent;


    #region Initializator
#if UNITY_EDITOR
    private enum FillType
    {
        Hierarchy,
        Manual
    }

    [SerializeField]
    private FillType fillType;


    public bool TryInitializate()
    {
        switch (fillType)
        {
            case FillType.Hierarchy:
                {
                    try
                    {
                        _placeAgent = GetComponent<PlaceAgent>();
                        _scholars = SIC<Scholar>.ComponentsDown(this.transform);

                        if(!_placeAgent.TryInitializate())
                            return false;                      

                        ScholarComputer desk;
                        ScholarMove scholarMove;

                        for(int i = 0; i< _scholars.Length; i++)
                        {
                            scholarMove = _scholars[i].GetComponent<ScholarMove>();
                            scholarMove.Position(_placeAgent.Places[PlaceManager.place.DockStation][i].Destination);
                            scholarMove.Rotation(_placeAgent.Places[PlaceManager.place.DockStation][i].SightGoal);

                            desk = _placeAgent.Places[PlaceManager.place.Desk][i].GetComponent<ScholarComputer>();
                            _scholars[i].SetDesk(desk);
                            _scholars[i].SetClass(this);

                        }

                        return true;
                    }
                    catch
                    {
                        return false;
                    }

                }
            case FillType.Manual:
                {
                    return true;
                }
        }

        return false;
    }

#endif
    #endregion

    public void SetLevel()
    {
    }



}

