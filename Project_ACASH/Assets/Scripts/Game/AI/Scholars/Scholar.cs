using UnityEngine;
using Overwatch.Memorable;
using AI.Scholars.Actions;
using AI.Scholars.Items;
using Objects._2D.ClassRoom;
using AI.Scholars.GOAP;
using AI.Scholars.Computer;
using GOAP;

namespace AI.Scholars
{
    public class Scholar : MonoBehaviour
    {
        public bool Active { get; private set; }



        public ClassAgent ClassRoom { get; private set; }
        public IGOAPStateReadOnlyStorageList GoapContext { get; private set; }
        public ScholarComputer Computer { get; private set; }
        public ScholarActionsController Actions { get; private set; }
        public ScholarMemorable Memorable { get; private set; }
        public ScholarInfo Info { get; private set; }
        public ScholarLocation Location { get; private set; }
        public ScholarItemsController Items { get; private set; }
        public ScholarMove Move => _move;
        public ScholarText Text => _text;
        public ScholarSightController Sight => _sight;


        [SerializeField] private ScholarMove _move;
        [SerializeField] private ScholarText _text;
        [SerializeField] private ScholarSightController _sight;





        public void Setup(ClassAgent classRoom, int globalIndex, int localIndex)
        {
            ClassRoom = classRoom;


            Active = true;

            Move.Setup(this);
            Sight.Setup(this);
            Computer = new ScholarComputer();
            Info = new ScholarInfo(this, globalIndex, localIndex);
            Items = new ScholarItemsController(this);
            Memorable = new ScholarMemorable(this);
            Location = new ScholarLocation(this);
            GoapContext = new ScholarGOAPContext(this);
            Actions = new ScholarActionsController(this);
        }

        public void Reset()
        {
            
        }

        public void MyUpdate()
        {
            if (Active)
            {

            }

            Move.MyUpdate();
            Sight.MyUpdate();
        }

        public void FixUpdate()
        {
            Move.FixUpdate();
        }

        public void SetNewScholar()
        {
            Info.SetFullName("Egor", "Akimov");
        }

        public void Pause(bool option)
        {

        }
    }
}
