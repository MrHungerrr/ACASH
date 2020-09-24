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

        public enum ExamStage
        {
            Prepare,
            Exam,
            Afterhours
        }

        public bool IsExam => _isExam;
        public ExamStage ExamPart => _examPart;

        public event Action OnExamStart;
        public event Action OnExamEnd;

        private Timer _examTimer;
        private bool _isExam;
        private ExamStage _examPart;


        public void SetupSchool()
        {
            _examPart = ExamStage.Afterhours;
        }


        public void UnsetupSchool()
        {
            OnExamStart = null;
            OnExamEnd = null;
        }


        public void SetExam(int examDuration, int cheatCount, int distractionCount)
        {
            if (_examPart != ExamStage.Afterhours)
                throw new Exception("Предыдущий экзамен еще не закончился");

            _examTimer = new Timer(examDuration);
            _isExam = false;
            _examPart = ExamStage.Prepare;

            var scheduler = new ExamEventScheduler(_examTimer);

            if(cheatCount > 0)
                scheduler.SetRandomSchedule(cheatCount, ExamEventExecuter.Cheat);

            if (distractionCount > 0)
                scheduler.SetRandomSchedule(distractionCount, ExamEventExecuter.Distraction);
        }

        public void StartExam()
        {
            if (_examPart != ExamStage.Prepare)
                throw new Exception("Запуск экзамена без этапа подготовки");

            _isExam = true;
            _examPart = ExamStage.Exam;

            _examTimer.OnTimeDone += FinishExam;
            _examTimer.Start();

            OnExamStart?.Invoke();
        }

        private void FinishExam()
        {
            if (_examPart != ExamStage.Exam)
                throw new Exception("Завершение экзамена без самого экзамена");

            _isExam = false;
            _examPart = ExamStage.Afterhours;

            OnExamEnd?.Invoke();
        }
    }
}