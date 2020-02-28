using System.Collections;
using UnityEngine;
using System.Linq;
using TMPro;

public class ActionsTest: MonoBehaviour
{

    public Scholar scholar;
    public TextMeshProUGUI text;


    private string operation;
    private int index = 0;



    public void Start()
    {
        index = 0;
        GetOperation();
    }

        
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DoAction();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Plus();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Minus();
        }
    }


    private void Plus()
    {
        index++;

        if (index >= OperationsList.operations.Count)
        {
            index = 0;
        }

        GetOperation();
    }


    private void Minus()
    {
        index--;

        if (index < 0)
        {
            index = OperationsList.operations.Count - 1;
        }

        GetOperation();
    }


    private void GetOperation()
    {
        operation = OperationsList.operations.ElementAt(index).Key;
        text.text = operation;
    }
    


    public void DoAction()
    {
        scholar.Action.DoAction(operation);
    }

}

