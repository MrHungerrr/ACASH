using System.Collections;
using UnityEngine;
using ComputerActions;


public class ScholarComputerTyping : MonoBehaviour
{
    private ScholarComputer Comp;
    [HideInInspector]
    public bool typing { get; private set; }



    public void Setup(ScholarComputer computer)
    {
        Comp = computer;
    }




    private IEnumerator Type(string number)
    {
        typing = true;

        Debug.Log("Type - " + number);

        if (number != null)
        {
            for(int i = 0; i < number.Length; i++)
            {
                Comp.Commands.Do(number[i].ToString());
                yield return new WaitForSeconds(Random.Range(0.4f, 0.8f));
            }
        }

        typing = false;
    }



    public void Clear()
    {
        if (Comp.Numpad.IsEnabled())
        {
            Comp.Commands.Do(GetC.commands.Reset);
        }
        else
        {
            Debug.LogError("Не включен Numpad");
        }
    }

    public void TypeSmart(FiveDigitInt digits)
    {
        string number = digits.ToString();

        if(Comp.Numpad.IsEnabled())
        {
            if (Comp.Numpad.input_field.length >= number.Length)
            {
                for (int i = 0; i < (Comp.Numpad.input_field.length - number.Length); i++)
                {
                    number = '0' + number;
                }

                StartCoroutine(Type(number));
            }
            else
            {
                Debug.LogError("Слишком большое число - " + number + "\nМаксимальная длинна - " + Comp.Numpad.input_field.length + "цифр");
            }
        }
        else
        {
            Debug.LogError("Не включен Numpad");
        }
    }

    public void Type(FiveDigitInt digits)
    {
        string number = digits.ToString();

        if (Comp.Numpad.IsEnabled())
        {
            StartCoroutine(Type(number));
        }
        else
        {
            Debug.LogError("Не включен Numpad");
        }
    }


}
