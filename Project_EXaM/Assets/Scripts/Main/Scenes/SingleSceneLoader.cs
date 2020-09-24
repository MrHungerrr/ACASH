using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public sealed class SingleSceneLoader
    {

        public event Action OnLoaded;
        public bool IsLoading { get; private set; }


        private int? _currentSceneBuildIndex;


        public SingleSceneLoader()
        {
            _currentSceneBuildIndex = null;
        }


        public void Set(string sceneName)
        {
            Debug.Log("Set Poshel");
            int index = ScenesManager.Instance.GetSceneIndexByName(sceneName);
            Debug.Log($"Получили индекс сцены - {index}");
            Set(index);
        }

        public void Set(int sceneBuildIndex)
        {
            if (IsLoading)
                throw new Exception("Еще не загружена другая сцена");

            if (_currentSceneBuildIndex.HasValue)
                Unload(_currentSceneBuildIndex.Value);

            Debug.Log("Грузим сцену");
            Load(sceneBuildIndex);
        }

        private void Load(int sceneBuildIndex)
        {
            if (SceneManager.GetSceneByBuildIndex(sceneBuildIndex).isLoaded)
            {
                Debug.LogError("Сцена уже загружена!");
                throw new Exception("Сцена уже загружена!");
            }

            var loadingOperation = SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Additive);

            if (loadingOperation.isDone)
                SceneLoaded(loadingOperation);
            else
                loadingOperation.completed += SceneLoaded;

            _currentSceneBuildIndex = sceneBuildIndex;
            IsLoading = true;
        }

        private void Unload(int sceneBuildIndex)
        {
            if (!SceneManager.GetSceneByBuildIndex(sceneBuildIndex).isLoaded)
                throw new Exception("Пытаемся выгрузить не загруженную сцену!");

            SceneManager.UnloadSceneAsync(sceneBuildIndex);
        }

        private void SceneLoaded(AsyncOperation loadOperation)
        {
            loadOperation.completed -= SceneLoaded;
            IsLoading = false;

            if (!loadOperation.isDone)
                throw new Exception("Сцена не загружена!");

            OnLoaded?.Invoke();
        }
    }
}
