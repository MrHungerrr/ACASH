using UnityEngine;
using Objects.Organization.Places;


public class SortAgent: MonoBehaviour
{

    [SerializeField]
    private SortManager.MainCoordinate MainLine;

    [SerializeField]
    private SortManager.TypeOfSort FirstObjectX;

    [SerializeField]
    private SortManager.TypeOfSort FirstObjectZ;


    public void Sort(Place[] places)
    {
        SortManager.Instance.Sort(MainLine, FirstObjectX, FirstObjectZ, places);
    }
}
