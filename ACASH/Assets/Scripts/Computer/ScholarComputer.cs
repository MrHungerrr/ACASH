using UnityEngine;


public class ScholarComputer : Computer
{

    public override void SetComputer()
    {
        base.SetComputer();
    }


    public override void SetScholars()
    {
        base.SetScholars();
    }

    public void Select(string key)
    {
        select = key;
        base.Select();
    }

}
