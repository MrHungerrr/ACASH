using System;
using System.Threading;
using System.Collections.Generic;


namespace MultiTasking
{
    public static class ThreadTaskQueuer
    {
        private static Queue<Action> _tasks;
        private static Thread _thread;


        public static void Setup()
        {
            _thread = new Thread(new ThreadStart(() => { }));
        }


        public static void SetLevel()
        {
            _tasks = new Queue<Action>();
        }


        public static void AddTask(Action task)
        {
            _tasks.Enqueue(task);
        }


        public static void Update()
        {
            if (_thread.IsAlive)
            {

            }
            else
            {
                if (_tasks.Count > 0)
                {
                    NextTask();
                }
            }
        }

        private static void NextTask()
        {
            var task = _tasks.Dequeue();
            _thread = new Thread(new ThreadStart(task));
            _thread.Start();
        }

    }
}

