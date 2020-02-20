using UnityEngine;
using System.Collections;
using TMPro;

public interface I_ObjectSelect
{
    void Select();

    void Deselect();

    bool CanISelect();
}
