using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vkimow.Tools.Single;

namespace Scenes
{
    public sealed class ScenesManager : Singleton<ScenesManager>
    {
        private readonly Dictionary<string, int> _scenesNameIndexPair;


        public ScenesManager()
        {
            _scenesNameIndexPair = new Dictionary<string, int>();

            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                int lastSlash = scenePath.LastIndexOf("/");
                string name = scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1);
                _scenesNameIndexPair.Add(name, i);
            }
        }

        public bool ContainsScene(string name)
        {
            return _scenesNameIndexPair.ContainsKey(name);
        }

        public int GetSceneIndexByName(string name)
        {
            if (!_scenesNameIndexPair.ContainsKey(name))
                throw new ArgumentException();

            return _scenesNameIndexPair[name];
        }
    }
}
