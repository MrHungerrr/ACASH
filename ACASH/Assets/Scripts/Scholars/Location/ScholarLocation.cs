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
        GoTo(place, index);

        PlaceManager.get.MakeFree(this.place, this.index);
        PlaceManager.get.MakeBusy(place, index);

        this.place = place;
        this.index = index;
    }

    private void GoTo(PlaceManager.place place, int index)
    {
        Vector3 destination = PlaceManager.get.GetPlace(place, index);
        Scholar.Move.SetDestination(destination);
    }


    public void Teleport(PlaceManager.place place, int index)
    {
        if(this.index != -1)
            PlaceManager.get.MakeFree(this.place, this.index);


        PlaceManager.get.MakeBusy(place, index);

        Scholar.Move.Position(PlaceManager.get.GetPlace(place, index));
        Scholar.Move.Rotation(PlaceManager.get.GetSightGoal(place, index));

        this.place = place;
        this.index = index;
    }


}

