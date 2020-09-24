using System;
using System.Threading.Tasks;
using UnityEngine;



namespace Levels
{
    public abstract class Level : MonoBehaviour
    {
        public event Action OnSetuped;
        public bool IsStarted { get; private set; } = false;
        public bool IsSetuped { get; private set; } = false;


        protected string _levelName = "No Name";


        public async Task LoadAndSetup()
        {
            await Load();
            await Setup();
            Debug.Log("Level Setuped");
            IsSetuped = true;
            OnSetuped?.Invoke();
        }


        public void LevelStart()
        {
            Debug.Log("Level Start");
            IsStarted = true;
            Action();
        }

        protected abstract Task Load();

        protected abstract Task Setup();

        protected abstract void Action();

        public sealed override string ToString()
        {
            return _levelName;
        }
    }
}
