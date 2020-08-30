using UnityEngine;
using Searching;
using Overwatch.Memorable;


namespace AI.Scholars
{
    public class ScholarOperations
    {
        private Scholar _scholar; 

        public ScholarOperations(Scholar scholar)
        {
            _scholar = scholar;
        }


        public void SetNewScholar()
        {
            _scholar.Info.SetFullName("Egor", "Akimov");
        }

        public void StartExam()
        {
            _scholar.Location.GoToDesk();
        }

        public void Pause(bool option)
        {

        }

        public void Execute()
        {
            _scholar.Location.GoToDockStation();
        }
    }
}
