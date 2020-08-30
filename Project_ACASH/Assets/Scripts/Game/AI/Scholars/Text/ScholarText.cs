using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AI.Scholars
{
    public class ScholarText : MonoBehaviour
    {
        [SerializeField] TextMeshPro _numberField;

        public void SetNumber(int number)
        {
            _numberField.text = number.ToString();
        }
    }
}
