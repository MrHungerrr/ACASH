using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using N_BH;

public class LevelManager : Singleton<LevelManager>
{
    private string activeLevel;

    public void Load(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            activeLevel = sceneName;
        }
    }

    public void LoadFast(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void Unload(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.UnloadSceneAsync(sceneName); 
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
