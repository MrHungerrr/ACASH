using System.Collections;
using UnityEngine;

public class TutorialKeyHint_1: KeyHint
{

    public Door first_door;
    public HintController hint_rotate;
    public HintController hint_move;
    public HintController hint_action;


    public void Begin()
    {
        hint_move.SetHint(true);
        hint_rotate.SetHint(true);
        hint_action.SetHint(false);
        hint_action.Enable();

        StartCoroutine(Begining());
    }



    private IEnumerator Begining()
    {
        while (!first_door.open)
        {
            yield return new WaitForEndOfFrame();
        }

        hint_action.Disable();
    }

}
