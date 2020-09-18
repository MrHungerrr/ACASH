using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using GameTime;
using System;
using UnityEngine;
using Vkimow.Tools.Single;
using Overwatch.Memorable;
using Overwatch;
using Overwatch.Read;
using Exam.Events;

namespace Exam
{
    public class ExamManager : Singleton<ExamManager>
    {
        public const int EXAM_TIME = 120;

        public enum part
        {
            Prepare,
            Exam,
            Afterhours,
        }

        public bool Exam => _exam;
        public part ExamPart => _examPart;

        public event Action OnExamStart;
        public event Action OnExamEnd;

        private Timer _examTimer;
        private bool _exam;
        private part _examPart;


        public void SetLevel()
        {
            ExamStarter.Instance.SetLevel();
            ExamStarter.Instance.AddListener(StartExam);
        }


        public void UnsetLevel()
        {
            OnExamStart = null;
            OnExamEnd = null;
        }


        public void ResetExam(int examDuration, int cheatCount, int specialCount)
        {
            _examTimer = new Timer(examDuration);
            _exam = false;
            _examPart = part.Prepare;

            var scheduler = new ExamEventScheduler(_examTimer);

            if(cheatCount > 0)
                scheduler.SetSchedule(cheatCount, "Cheat");

            if (specialCount > 0)
                scheduler.SetSchedule(specialCount, "Special");
        }

        private void StartExam()
        {
            if (_examPart != part.Prepare)
                throw new ArgumentException();

            _exam = true;
            _examPart = part.Exam;

            _examTimer.OnTimeDone += FinishExam;
            _examTimer.Start();

            OnExamStart?.Invoke();
        }

        private void FinishExam()
        {
            if (_examPart != part.Exam)
                throw new ArgumentException();

            _exam = false;
            _examPart = part.Afterhours;

            OnExamEnd?.Invoke();
        }
    }
}