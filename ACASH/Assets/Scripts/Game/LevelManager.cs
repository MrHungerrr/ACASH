using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Single;

public class LevelManager : Singleton<LevelManager>
{

    public enum levels
    {
        Main,
        Elevator,
        Tutorial_1,
        Tutorial_2,
        Level_1,
        Level_2,
    }



    public bool loading = false;
    public int current_level { get; private set; }

    private ActionEvent.OnAction OnLoad;


    public void Update()
    {
        if(loading)
        {
            IsLoading();
        }
    }


    public void LoadInstead(levels level)
    {
        LoadInstead((int)level);
    }

    private void LoadInstead(int scene_index)
    {
        if (!SceneManager.GetSceneByBuildIndex(scene_index).isLoaded)
        {
            Unload(current_level);
            Load(scene_index);
        }
    }

    public void Load(levels level)
    {
        Load((int)level);
    }

    private void Load(int scene_index)
    {
        try
        {
            if (!SceneManager.GetSceneByBuildIndex(scene_index).isLoaded)
            {
                loading = true;

                OnLoad += GameManager.get.StartLevel;
                SceneManager.LoadSceneAsync(scene_index, LoadSceneMode.Additive);

                current_level = scene_index;
            }
        }
        catch
        {
            Debug.LogError("Level Manager. Bad Index - " + scene_index);
        }
    }

    /* private void Load(string level)
     {
         try
         {
             if (!scene.isLoaded)
             {
                 loading = true;
                 current_level = scene.buildIndex;
                 OnLoad += GameManager.get.StartLevel;
                 SceneManager.LoadSceneAsync(current_level, LoadSceneMode.Additive);
             }
         }
         catch
         {
             Debug.LogError(scene.name);
         }
     }
     */


    private void IsLoading()
    {
        if(SceneManager.GetSceneByBuildIndex(current_level).isLoaded)
        {
            loading = false;

            if(OnLoad != null)
            {
                ActionEvent.Unsubscribe(OnLoad);
                OnLoad();
            }
        }
    }


    public void Unload(levels level)
    {
        string scene_name = GetPath(level);
        Unload(SceneManager.GetSceneByName(scene_name).buildIndex);
    }

    private void Unload(int scene_index)
    {
        if (SceneManager.GetSceneByBuildIndex(scene_index).isLoaded)
            SceneManager.UnloadSceneAsync(scene_index);
    }


    public void UnloadLevels()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            Unload(i);
        }
    }




    private string GetPath(levels level)
    {
        return level.ToString();
    }

}
