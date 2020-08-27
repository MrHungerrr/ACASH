using UnityEngine;
using System.Collections;
using Places;

public class ScholarLocation
{
    public Place CurrentPlace => _currentPlace;


    private Scholar _scholar;
    private Place _currentPlace;

    public ScholarLocation(Scholar Scholar)
    {
        this._scholar = Scholar;
        _currentPlace = null;
    }

    public void ChangeLocation(Place place)
    {
        try
        {
            GoTo(place);

            _currentPlace?.SetBusy(false);
            place.SetBusy(true);
        }
        catch
        {
            Debug.LogError( "Не существующее место "+ place.ToString() + " в ScholarLocation");
        }

        _currentPlace = place;
    }

    private void GoTo(Place place)
    {
        Vector3 destination = place.Destination;
        _scholar.Move.SetDestination(destination);
    }


    public void Teleport(Place place)
    {
        _currentPlace?.SetBusy(false);

        try
        {
            place.SetBusy(true);

            _scholar.Move.Position(place.Destination);
            _scholar.Move.Rotation(place.SightGoal);
        }
        catch
        {
            Debug.LogError("Не существующее место " + place.ToString() + " в ScholarLocation");
        }

        _currentPlace = place;
    }


}

