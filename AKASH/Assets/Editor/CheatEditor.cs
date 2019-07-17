using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CheatingScript))]
public class CheatEditor : Editor
{



    public override void OnInspectorGUI()
    {
        var _CheatingScript = target as CheatingScript;

        _CheatingScript.isUnderTeacherSupervision = EditorGUILayout.Toggle("PALIVO", _CheatingScript.isUnderTeacherSupervision);
        _CheatingScript.stress = EditorGUILayout.Slider("Stress", _CheatingScript.stress, 0, 100);

        
        if (_CheatingScript.Mudak)
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
        else if (!_CheatingScript.Mudak || !_CheatingScript.Terpila || !_CheatingScript.Daun)
        {
            _CheatingScript.Mudak = EditorGUILayout.Toggle("Mudak", _CheatingScript.Mudak);
            _CheatingScript.Terpila = EditorGUILayout.Toggle("Terpila", _CheatingScript.Terpila);
            _CheatingScript.Daun = EditorGUILayout.Toggle("Daun", _CheatingScript.Daun);
        }


        serializedObject.ApplyModifiedProperties();
    }
}
