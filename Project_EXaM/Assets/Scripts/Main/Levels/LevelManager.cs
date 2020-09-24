using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Vkimow.Tools.Single;
using Vkimow.Unity.Tools.Single;

namespace Levels
{
    public sealed class LevelManager : MonoSingleton<LevelManager>
    {
        public event Action OnLevelStarted;
        private Level _currentLevel;


        public void SetLevel<T>() where T : Level
        {
            if(_currentLevel != null)
                Destroy(_currentLevel);

            _currentLevel = gameObject.AddComponent<T>();
            _currentLevel.OnSetuped += StartLevel;
            _currentLevel.LoadAndSetup();
        }

        private void StartLevel()
        {
            _currentLevel.OnSetuped -= StartLevel;

            if (_currentLevel.IsStarted)
                throw new Exception($"Level \"{_currentLevel}\" Already Started");

            _currentLevel.LevelStart();
            OnLevelStarted?.Invoke();
        }
    }
}
