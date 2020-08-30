using UnityEngine;
using Searching;
using Overwatch.Memorable;


namespace AI.Scholars
{
    public class Scholar : MonoBehaviour
    {
        public bool Active { get; private set; }


        public ClassAgent ClassRoom { get; private set; }
        public ScholarOperations Operations { get; private set; }
        public ScholarMemorable Memorable { get; private set; }
        public ScholarInfo Info { get; private set; }
        public ScholarLocation Location { get; private set; }
        public ScholarMove Move => _move;
        public ScholarText Text => _text;
        public ScholarSightController Sight => _sight;


        [SerializeField] private ScholarMove _move;
        [SerializeField] private ScholarText _text;
        [SerializeField] private ScholarSightController _sight;





        public virtual void Setup(ClassAgent classRoom, int globalIndex, int localIndex)
        {
            ClassRoom = classRoom;

            Active = true;

            Move.Setup(this);
            Sight.Setup(this);

            Info = new ScholarInfo(this, globalIndex, localIndex);
            Operations = new ScholarOperations(this);
            Memorable = new ScholarMemorable(this);
            Location = new ScholarLocation(this);

        }

        public void MyUpdate()
        {
            if (Active)
            {
            }

            Sight.MyUpdate();
        }

        public void FixUpdate()
        {
            Move.FixUpdate();
        }
    }
}
