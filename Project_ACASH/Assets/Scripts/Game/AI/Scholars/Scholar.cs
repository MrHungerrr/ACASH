using UnityEngine;
using Overwatch.Memorable;
using AI.Scholars.Actions;
using AI.Scholars.Actions.Operations;
using AI.Scholars.Items;

namespace AI.Scholars
{
    public class Scholar : MonoBehaviour
    {
        public bool Active { get; private set; }


        public ClassAgent ClassRoom { get; private set; }
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

            Info = new ScholarInfo(this, globalIndex, localIndex);
            Actions = new ScholarActionsController(this);
            Memorable = new ScholarMemorable(this);
            Location = new ScholarLocation(this);
        }

        public void Reset()
        {
            
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

        public void SetNewScholar()
        {
            Info.SetFullName("Egor", "Akimov");
            Items = new ScholarItemsController(null);
        }

        public void Pause(bool option)
        {

        }
    }
}
