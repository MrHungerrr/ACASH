using UnityEngine;
using System.Collections;

public class ScholarLocation
{
    public PlaceManager.place place;
    public int index;

    public void ChangeLocation(PlaceManager.place place, int index)
    {
        PlaceManager.get.MakeFree(this.place, this.index);
        PlaceManager.get.MakeBusy(place, index);
        this.place = place;
        this.index = index;
    }



}

