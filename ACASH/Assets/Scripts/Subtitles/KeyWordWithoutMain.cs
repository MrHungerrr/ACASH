using UnityEngine;

public class KeyWordWithoutMain: KeyWord
{

    public KeyWordWithoutMain(string const_key) : base(string.Empty, const_key)
    {
    }


    public override string GetFullWord()
    {
        return GetFullKey();
    }

}
