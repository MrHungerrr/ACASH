using System.Collections;
using PlayerOptions;
using UnityEngine;
using UnityTools.Single;

public abstract class A_Level : MonoSingleton<A_Level>
{


    protected KeyWord key;
    protected KeyWord key_mistake;


    protected void Key(string level)
    {
        key = new KeyWord(level);
        key_mistake = new KeyWord(level, "Mistake");
    }

    public virtual void StartLevel()
    {
        Setup();
        Begin();
    }

    protected abstract void Setup();

    protected abstract void Begin();
}
