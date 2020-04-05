using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Single;

public class LevelManager : Singleton<LevelManager>
{
    public string current_level { get; private set; }

    public void LoadInstead(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            Unload(current_level);

            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            if (sceneName != "Elevator")
                current_level = sceneName;
        }
    }

    public void Load(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            if (sceneName != "Elevator")
                current_level = sceneName;
        }
    }

    public void LoadFast(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

            if(sceneName != "Elevator")
                current_level = sceneName;
        }
    }

    public void Unload(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.UnloadSceneAsync(sceneName); 
    }

    public bool IsLoad()
    {
        return SceneManager.GetSceneByName(current_level).isLoaded;
    }

    public bool IsLoad(string sceneName)
    {
        return SceneManager.GetSceneByName(sceneName).isLoaded;
    }


    public void UnloadLevels()
    {
        for(int i = 1; i< SceneManager.sceneCountInBuildSettings; i++)
        {
            if (SceneManager.GetSceneByBuildIndex(i).isLoaded)
                SceneManager.UnloadSceneAsync(i);
        }
    }
}
