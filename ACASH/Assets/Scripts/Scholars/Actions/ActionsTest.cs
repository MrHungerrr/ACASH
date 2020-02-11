using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionsTest : MonoBehaviour
{

    public Scholar Scholar;
    private TextMeshProUGUI text_set_type;
    private TextMeshProUGUI text_set_action;
    private TextMeshProUGUI text_type;
    private TextMeshProUGUI text_stress;
    private TextMeshProUGUI text_action;
    private TextMeshProUGUI text_mood;
    private GameObject help;
    private int type_nom;
    private int stress;
    private int act_nom;



    private string[] types = new string[]
    {
        "Dumb",
        "Asshole"
    };



    private string[] actions = new string[]
    {
        "Toilet_1",
        "Cheating_1",
    };



    void Start()
    {
        text_set_type = transform.Find("Text").transform.Find("Set Menu").transform.Find("Type Text").GetComponent<TextMeshProUGUI>();
        text_set_action = transform.Find("Text").transform.Find("Set Menu").transform.Find("Action Text").GetComponent<TextMeshProUGUI>();

        text_type = transform.Find("Text").transform.Find("Scholar Stats").transform.Find("Type Text").GetComponent<TextMeshProUGUI>();
        text_stress = transform.Find("Text").transform.Find("Scholar Stats").transform.Find("Stress Text").GetComponent<TextMeshProUGUI>();
        text_action = transform.Find("Text").transform.Find("Scholar Stats").transform.Find("Action Text").GetComponent<TextMeshProUGUI>();
        text_mood = transform.Find("Text").transform.Find("Scholar Stats").transform.Find("Mood Text").GetComponent<TextMeshProUGUI>();
        help = transform.Find("Text").transform.Find("Hide Help").gameObject;

        act_nom = 0;
        Text();
        help.SetActive(false);
    }


    private void Update()
    {
        ScholarControl();
        ScholarRefresh();
    }




    private void ScholarControl()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            SwitchAction(false);
        }

        if (Input.GetKeyDown(KeyCode.Insert))
        {
            SwitchAction(true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Scholar.Action.Doing(actions[act_nom]);
        }



        if (Input.GetKeyDown(KeyCode.End))
        {
            SwitchStress(false);
        }

        if (Input.GetKeyDown(KeyCode.Home))
        {
            SwitchStress(true);
        }



        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            SwitchType(false);
        }

        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            SwitchType(true);
        }

        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            Scholar.ChangeType(types[type_nom]);
        }



        if (Input.GetKeyDown(KeyCode.H))
        {
            help.SetActive(!help.activeSelf);
        }
    }




    private void SwitchType(bool greater)
    {
        if (greater)
        {
            type_nom++;

            if (type_nom >= types.Length)
                type_nom %= types.Length;
        }
        else
        {
            type_nom--;

            if (type_nom == -1)
                type_nom = types.Length - 1;
        }

        Text();
    }




    private void SwitchStress(bool greater)
    {
        if (greater)
        {
            Scholar.Stress.Change(10);
        }
        else
        {
            Scholar.Stress.Change(-10);
        }
    }




    private void SwitchAction(bool greater)
    {
        if (greater)
        {
            act_nom++;

            if (act_nom >= actions.Length)
                act_nom %= actions.Length;
        }
        else
        {
            act_nom--;

            if (act_nom == -1)
                act_nom = actions.Length - 1;
        }

        Text();
    }




    private void Text()
    {
        text_set_type.text = types[type_nom];
        text_set_action.text = actions[act_nom];
    }       


    private void ScholarRefresh()
    {
        text_type.text = Scholar.type;
        text_stress.text = Scholar.Stress.value.ToString();
        text_action.text = Scholar.Action.last_key_action;
        text_mood.text = Scholar.Stress.GetMoodType();
    }
}
