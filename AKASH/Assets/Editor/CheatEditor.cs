using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CheatingScript))]
public class CheatEditor : Editor
{
    CheatingScript _CheatingScript;

    public void OnEnable()
    {
        _CheatingScript = target as CheatingScript;
    }

    public override void OnInspectorGUI()
    {
        GUIStyle b = new GUIStyle();
        b.fontStyle = FontStyle.Bold;

        EditorGUILayout.LabelField("Факторы: ", b);
        //_CheatingScript.isUnderTeacherSupervision = EditorGUILayout.Toggle("PALIVO", _CheatingScript.isUnderTeacherSupervision);

        if (!_CheatingScript.Daun)
        _CheatingScript.stress = EditorGUILayout.Slider("Stress", _CheatingScript.stress, 0, 100);

        _CheatingScript.isUnderTeacherSupervision = EditorGUILayout.Toggle("VsevidisheeOKO", _CheatingScript.isUnderTeacherSupervision);

        
        EditorGUILayout.LabelField("Класс Ученика: ", b);

        if (!_CheatingScript.Mudak && !_CheatingScript.Terpila && !_CheatingScript.Daun)
        {
            _CheatingScript.Mudak = EditorGUILayout.Toggle("Mudak", _CheatingScript.Mudak);
            _CheatingScript.Terpila = EditorGUILayout.Toggle("Terpila", _CheatingScript.Terpila);
            _CheatingScript.Daun = EditorGUILayout.Toggle("Daun", _CheatingScript.Daun);
        }
        else if(_CheatingScript.Mudak)
        {
            _CheatingScript.Mudak = EditorGUILayout.Toggle("Mudak", _CheatingScript.Mudak);
        }
        else if (_CheatingScript.Terpila)
        {
            _CheatingScript.Terpila = EditorGUILayout.Toggle("Terpila", _CheatingScript.Terpila);
        }
        else if (_CheatingScript.Daun)
        {
            _CheatingScript.Daun = EditorGUILayout.Toggle("Daun", _CheatingScript.Daun);
        }

       // DrawDefaultInspector();
        serializedObject.ApplyModifiedProperties();
    }
}
