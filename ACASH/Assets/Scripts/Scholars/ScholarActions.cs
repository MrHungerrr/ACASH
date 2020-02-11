using System.Collections;
using UnityEngine;

public class ScholarActions
{

    private Scholar Scholar;
    private ActionsSripts Actions;


    [HideInInspector]
    public string key_action;
    [HideInInspector]
    public string last_key_action;


    [HideInInspector]
    public Vector3 home;
    [HideInInspector]
    public Vector3 desk;



    public ScholarActions(Scholar scholar)
    {
        Scholar = scholar;
        Actions = new ActionsSripts(scholar);
        key_action = null;
    }



    
 

}

