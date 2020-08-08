using UnityEngine;
using System.Collections;
using TMPro;

public interface IObjectSelect
{
    void Select();

    void Deselect();

    bool CanISelect();
}
