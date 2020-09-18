using AI.Scholars;
using AI.Scholars.GOAP;
using GameTime;
using GameTime.Action;
using System;
using System.Collections.Generic;
using Vkimow.Tools.Extensions;
using Vkimow.Tools.Single;

namespace Exam.Events
{
    public class ExamEventScheduler
    {
        private Timer _timer;

        public ExamEventScheduler(Timer timer)
        {
            _timer = timer;
        }

        public void SetSchedule(int eventCount, string eventKey)
        {
            var deltaEnd = _timer.TimeGeneral / 10;
            var scheduleTime = GetRandomEventSchedule(eventCount, _timer.TimeGeneral - deltaEnd);
            
            for(int i = 0; i < eventCount; i++)
            {
                Action action = () =>
                {
                    ExamEventExecuter.Instance.Execute(eventKey);
                };

                _timer.Schedule.AddActionAtTime(scheduleTime[i], action);
            }
        }

        private int[] GetRandomEventSchedule(int eventCount, int totalTime)
        {
            Random random = new Random();

            var schedule = new int[eventCount];
            int averageTime = totalTime / eventCount;

            schedule[eventCount / 2] = averageTime;

            for(int i = 0; i < eventCount/2; i++)
            {
                int deltaTime =  random.Next(averageTime);
                schedule[i] = averageTime + deltaTime;
                schedule[eventCount - i - 1] = averageTime - deltaTime;
            }

            schedule.Shuffle();

            for (int i = 1; i < eventCount; i++)
            {
                schedule[i] += schedule[i - 1];
            }

            return schedule;
        }
    }
}
