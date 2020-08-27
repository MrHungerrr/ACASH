using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScholarInfo : MonoBehaviour
{
    public int Number { get; private set; }
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string FullName => $"{Name} {Surname}";


    [SerializeField] private TextMeshPro _numberField;
    [SerializeField] private TextMeshProUGUI _minimapNumberField;


    public void Setup(int number, string name, string surname)
    {
        SetNumber(number);
        SetFullName(name, surname);
    }

    private void SetFullName(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }

    private void SetNumber(int number)
    {
        Number = number;

        var numberString = (number + 1).ToString();
        _numberField.text = numberString;
        _minimapNumberField.text = numberString;
    }


}
