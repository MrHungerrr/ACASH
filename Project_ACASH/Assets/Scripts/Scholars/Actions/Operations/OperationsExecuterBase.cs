using UnityEngine;
using System.Collections;
using Animations;
using Places;

public abstract class OperationsExecuterBase : MonoBehaviour
{
    protected Scholar _scholar;
    protected OperationsManager _manager;

    public delegate bool VerifyDelegate();

    /*
    protected int actionNum;
    protected int actionNoPlus;
    */


    public void SetOperationsExecuter(Scholar Scholar, OperationsManager Manager)
    {
        this._scholar = Scholar;
        this._manager = Manager;
    }


    public void Do(string key)
    {
        Stop();
        StartCoroutine(key);
    }

    public void Do(string key, int i)
    {
        Stop();
        StartCoroutine(key, i);
    }

    public void Do(string key, string option)
    {
        Stop();
        StartCoroutine(key, option);
    }



    public void Stop()
    {
        _scholar.Anim.SetAnimation(Get.animations.Nothing);
        StopAllCoroutines();
    }


    protected void OperationEnd()
    {
        _manager.OperationDone();
    }


    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //Продолжить незаконченное действие

    /* public void Continue(string key)
     {
         actionNoPlus = 0;
         StartCoroutine(key);
         doing = true;
     }
     */



    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //Функция для запоминания этапа действия , если действие прервется
    /*
    protected bool CanIContinue()
    {
        if (actionNum == actionNoPlus)
        {
            actionNoPlus++;
            return true;
        }
        else
        {
            actionNoPlus++;
            return false;
        }
    }
    */




    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //Зрение

    protected void WatchTo(Place place)
    {
        Watch(place.SightGoal);
    }

    protected void Watch(Vector3 target)
    {
        _scholar.Move.SetRotateGoal(target);
    }

    protected void Watch(float angle)
    {
        _scholar.Move.SetRotateGoal(angle);
    }

    protected void Watch(Quaternion targetRotation)
    {
        _scholar.Move.SetRotateGoal(targetRotation);
    }

    protected bool SightIsHere()
    {
        return !_scholar.Move.Rotating;
    }






    //=========================================================================================================================================================
    //=========================================================================================================================================================
    //Местоположение и передвижение

    public void GoTo(Place place)
    {
        Stop();
        StartCoroutine(Go_To(place));
    }

    protected bool IsHere()
    {
        return !_scholar.Move.Walking;
    }

    protected Quaternion GetRotation()
    {
        return _scholar.Move.Rotation();
    }

    protected Vector3 GetPosition()
    {
        return _scholar.Move.Position();
    }




    //=========================================================================================================================================================
    //=========================================================================================================================================================
    // Ожидание


    protected IEnumerator Wait(int option)
    {
        _scholar.Anim.SetAnimation(Get.animations.Nothing);
        yield return new WaitForSeconds(option);

        OperationEnd();
    }





    //=========================================================================================================================================================
    //=========================================================================================================================================================
    // Начало и конец списывание


    protected IEnumerator StartCheat()
    {
        _scholar.Cheat.StartCheat();
        yield return new WaitForEndOfFrame();

        OperationEnd();
    }

    protected IEnumerator EndCheat()
    {
        _scholar.Cheat.EndCheat();
        yield return new WaitForEndOfFrame();

        OperationEnd();
    }





    //=========================================================================================================================================================
    //=========================================================================================================================================================
    // Проверка, можно ли продолжать цепочку действий

    public bool VerifyToiletAreFree()
    {
        return _scholar.ClassRoom.PlaceAgent.HasFreePlace(PlaceManager.place.Toilet);
    }

    public bool VerifySinkAreFree()
    {
        return _scholar.ClassRoom.PlaceAgent.HasFreePlace(PlaceManager.place.Sink);
    }

    public bool VerifyOutsideAreFree()
    {
        return _scholar.ClassRoom.PlaceAgent.HasFreePlace(PlaceManager.place.Outside);
    }


    public void Verify(VerifyDelegate typeOfCheck, bool need_condition)
    {
        if (typeOfCheck() == need_condition)
            OperationEnd();
        else
            _manager.OperationsEnd();
    }




    //=========================================================================================================================================================
    // Идти домой

    private IEnumerator Go_Home(Place place)
    {
        _scholar.Location.ChangeLocation(place);

        while (!IsHere())
            yield return new WaitForEndOfFrame();

        WatchTo(place);
        _scholar.Body.Disable();

        yield return new WaitForEndOfFrame();

        _scholar.Move.RB.isKinematic = false;

        OperationEnd();
    }




    //=========================================================================================================================================================
    // Идти 

    protected IEnumerator Go_To(Place place)
    {
        GoTo(place);

        yield return new WaitForEndOfFrame();

        while (!IsHere())
            yield return new WaitForEndOfFrame();

        WatchTo(place);

        OperationEnd();
    }
}
