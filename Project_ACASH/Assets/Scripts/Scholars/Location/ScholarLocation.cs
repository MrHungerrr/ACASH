using UnityEngine;
using System.Collections;

public class ScholarLocation
{
    private Scholar Scholar;

    public PlaceManager.place place;
    public int index;

    public ScholarLocation(Scholar Scholar)
    {
        this.Scholar = Scholar;
        index = -1;
    }

    public void ChangeLocation(PlaceManager.place place, int index)
    {
        try
        {


            GoTo(place, index);

            PlaceManager.Instance.MakeFree(this.place, this.index);
            PlaceManager.Instance.MakeBusy(place, index);
        }
        catch
        {
            Debug.LogError( "Не существующее место "+ place.ToString() + " " + index + " в ScholarLocation");
        }

        this.place = place;
        this.index = index;
    }

    private void GoTo(PlaceManager.place place, int index)
    {
        Vector3 destination = PlaceManager.Instance.GetPlace(place, index);
        Scholar.Move.SetDestination(destination);
    }


    public void Teleport(PlaceManager.place place, int index)
    {
        if (this.index != -1)
            PlaceManager.Instance.MakeFree(this.place, this.index);

        try
        {
            PlaceManager.Instance.MakeBusy(place, index);

            Scholar.Move.Position(PlaceManager.Instance.GetPlace(place, index));
            Scholar.Move.Rotation(PlaceManager.Instance.GetSightGoal(place, index));
        }
        catch
        {
            Debug.LogError("Не существующее место " + place.ToString() + " " + index + " в ScholarLocation");
        }


        this.place = place;
        this.index = index;
    }


}

