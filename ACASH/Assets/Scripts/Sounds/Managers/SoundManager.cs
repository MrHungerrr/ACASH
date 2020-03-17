using UnityEngine;
using FMODUnity;

public class SoundManager : A_SoundSimple
{


    private static SoundManager _get;
    private static System.Object _lock = new System.Object();

    public static SoundManager get
    {
        get
        {
            if (_get == null)
            {
                
                lock(_lock)
                {
                    if(_get == null)
                    {
                        _get = new SoundManager();
                    }
                }
            }

            return _get;
        }
    }



    private SoundManager()
    {
        sounds_path += "Global/Sounds/";
    }


    public enum sound   
    {
        Fuck,
    }


    public void Make(sound sound)
    {
        base.Make(sound.ToString());
    }
}
