using UnityEngine;
using System;
using System.Threading.Tasks;
using Schools;
using Exam;

namespace Levels.Concrete
{
    public sealed class Level_1 : Level
    {
        protected override async Task Load()
        {
            await Task.Yield();
            return;
        }

        protected override async Task Setup()
        {
            _levelName = "Level 1";
            await Task.Yield();
            return;
        }

        protected override void Action()
        {
            SchoolManager.Instance.OnSchoolLoaded += ExamStart;
            SchoolManager.Instance.SetSchool(SchoolManager.School.School_1);
        }

        private void ExamStart()
        {
            SchoolManager.Instance.OnSchoolLoaded -= ExamStart;
            ExamManager.Instance.SetExam(15, 0, 2);
            ExamManager.Instance.StartExam();
        }
    }
}
