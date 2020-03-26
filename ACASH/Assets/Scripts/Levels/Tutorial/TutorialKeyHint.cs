using System.Collections;
using UnityEngine;
using PlayerOptions;

public class TutorialKeyHint: MonoBehaviour
{

    public Door first_door;
    public HintController hint_rotate;
    public HintController hint_move;
    public HintController hint_action;
    public HintController hint_zoom;


    public void Set(GetP.actions action)
    {
        HUDManager.get.HintHUD(action);
    }


    public void Disable()
    {
        HUDManager.get.CloseHintHUD();
    }



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



    public void Zoom()
    {
        hint_zoom.SetHint(true);
    }

}
