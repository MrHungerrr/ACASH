using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects._2D.ClassRoom;
using Objects._2D.Places;
using GameTime;
using MultiTasking;
using Vkimow.Unity.Tools.Single;
using Overwatch;
using Computers;
using AI.Scholars;
using Exam;
using Levels;
using Vkimow.Tools.Single;
using Scenes;

namespace Schools
{
    public class SchoolManager : Singleton<SchoolManager>
    {
        public enum School
        {
            School_1
        }

        public event Action OnSchoolLoaded;

        private School? _currentSchool;
        private SingleSceneLoader _schoolLoader;


        public SchoolManager()
        {
            _schoolLoader = new SingleSceneLoader();
            _schoolLoader.OnLoaded += SetupSchool;
            _currentSchool = null;
        }

        public void SetSchool(int schoolIndex)
        {
            School school;

            try
            {
                checked
                {
                    school = (School)schoolIndex;
                }
            }
            catch
            {
                throw new Exception("Неправильный индекс");
            }

            SetSchool(school);
        }

        public void SetSchool(School classRoom)
        {
            if (_currentSchool.HasValue)
                UnsetupSchool();

            _currentSchool = classRoom;
            _schoolLoader.Set(_currentSchool.ToString());
        }


        private void SetupSchool()
        {
            GameManager.Instance.SetGame(true);
            TimeManager.Instance.SetupSchool();
            ExamManager.Instance.SetupSchool();
            PlaceManager.SetupSchool();
            ScholarManager.Instance.SetupSchool();
            OverwatchManager.SetupSchool();
            ClassManager.Instance.SetupSchool();
            ComputerManager.Instance.SetupSchool();
            ExaminerRoomManager.Instance.SetupSchool();

            OnSchoolLoaded?.Invoke();
        }

        private void UnsetupSchool()
        {
            GameManager.Instance.SetGame(false);
            ExamManager.Instance.UnsetupSchool();
            AudioManager.Instance.UnsetupSchool();
        }
    }
}