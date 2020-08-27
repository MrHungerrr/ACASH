using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScholarInfo : MonoBehaviour
#region IInitialization 
#if UNITY_EDITOR
    , IInitialization
#endif
#endregion
{
    public int Number => _number;
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string FullName => $"{Name} {Surname}";


    [SerializeField] private int _number = -1;
    [SerializeField] private TextMeshPro _numberField;
    [SerializeField] private TextMeshProUGUI _minimapNumberField;
    
    
    #region Initialization
#if UNITY_EDITOR
    public bool AutoInitializate => false;

    public void Initializate()
    {
        if (_number == -1)
            throw new System.Exception("Неправильное значение поля _number");
    }
#endif
    #endregion

    public void Setup()
    {
        Surname = "Akimov";
        Name = "Egor";
        SetNumber(_number);
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetSurname(string surname)
    {
        Surname = surname;
    }

    public void SetFullName(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }

    public void SetNumber(int number)
    {
        _number = number;

        var numberString = (number + 1).ToString();
        _numberField.text = numberString;
        _minimapNumberField.text = numberString;
    }
}
