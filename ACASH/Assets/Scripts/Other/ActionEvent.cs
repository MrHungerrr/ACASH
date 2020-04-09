using System;
using UnityEngine.Events;


public static class ActionEvent
{

    public delegate void OnAction();
    public delegate void OnActionBool(bool option);

    /* public static void Unsubscribe(OnAction ConcreteDelegate)
     {
          if (ConcreteDelegate != null)
          {
              Delegate[] clientList = ConcreteDelegate.GetInvocationList();

             foreach (var d in clientList)
             {
                 ConcreteDelegate -= (d as OnAction);

                 Debug.Log(d.ToString()+ ", " + d.GetType().FullName);
             }
          }
      }

      public static void Unsubscribe(OnActionBool ConcreteDelegate)
      {

          ConcreteDelegate = null;


           if (ConcreteDelegate != null)
          {
              Delegate[] clientList = ConcreteDelegate.GetInvocationList();

              foreach (var d in clientList)
                  ConcreteDelegate -= (d as OnActionBool);
          }

     }
              */
}

