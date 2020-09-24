using UnityEngine;
using Objects._2D.Places;



namespace Objects._2D.Tools
{
    public class SortAgent2D : MonoBehaviour
    {

        [SerializeField]
        private SortManager2D.MainCoordinate MainLine;

        [SerializeField]
        private SortManager2D.TypeOfSort FirstObjectX;

        [SerializeField]
        private SortManager2D.TypeOfSort FirstObjectY;


        public void Sort(Place[] places)
        {
            SortManager2D.Instance.Sort(MainLine, FirstObjectX, FirstObjectY, places, x => x.Destination);
        }
    }
}
