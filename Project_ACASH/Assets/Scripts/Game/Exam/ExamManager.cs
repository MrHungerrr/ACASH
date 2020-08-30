using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using GameTime;
using System;
using UnityEngine;
using Single;
using Overwatch.Memorable;
using Overwatch;
using Overwatch.Read;



namespace Exam
{
    public class ExamManager : Singleton<ExamManager>
    {

        public enum part
        {
            Prepare,
            Exam,
            Afterhours,
        }

        public bool Exam => _exam;
        public part ExamPart => _examPart;

        public Action OnExamStart { get; set; }
        public Action OnExamEnd { get; set; }


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


        public void ResetExam()
        {
            _exam = false;
            _examPart = part.Prepare;
        }

        private void StartExam()
        {
            if (_examPart != part.Prepare)
                throw new ArgumentException();

            _exam = true;
            _examPart = part.Exam;
            TimeManager.Instance.SetTimer(10);
            TimeManager.Instance.Timer.OnTimeDone += FinishExam;

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