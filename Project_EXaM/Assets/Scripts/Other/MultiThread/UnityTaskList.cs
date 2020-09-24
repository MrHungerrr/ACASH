using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTasking
{
    public static class UnityTaskList
    {
        private static List<Action> _tasks;

        public static void Setup()
        {
            _tasks = new List<Action>();
        }


        public static void AddTask(Action task)
        {
            _tasks.Add(task);
        }


        public static void Update()
        {
            if (_tasks.Count > 0)
            {
                ExecuteTasks();
            }
        }

        private static void ExecuteTasks()
        {
            foreach(var task in _tasks)
            {
                task.Invoke();
            }

            _tasks = new List<Action>();
        }
    }
}
