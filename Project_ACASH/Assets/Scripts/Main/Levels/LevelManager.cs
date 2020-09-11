using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityTools.Single;

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

    private Action OnLoad;


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
            UnloadFast(current_level);
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
                OnLoad += GameManager.Instance.StartLevel;
                SceneManager.LoadSceneAsync(scene_index, LoadSceneMode.Additive);
                current_level = scene_index;
                loading = true;
            }
        }
        catch
        {
            Debug.LogError("Level Manager. Bad Index - " + scene_index);
        }
    }


    public void Reload()
    {
        UnloadFast(current_level);
        Load(current_level);
    }



    private void IsLoading()
    {
        if(SceneManager.GetSceneByBuildIndex(current_level).isLoaded)
        {
            if (loading)
            {
                loading = false;

                if (OnLoad != null)
                {
                    OnLoad();
                    OnLoad -= GameManager.Instance.StartLevel;
                }
            }
        }
    }

    private void IsUnloading()
    {
        if (!SceneManager.GetSceneByBuildIndex(current_level).isLoaded)
        {
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


    private void UnloadFast(int scene_index)
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
