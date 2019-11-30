using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuObject : MonoBehaviour
{
    [SerializeField]
    private MenuSection[] sections;

    private void OnEnable()
    {
        Menu.get.Set(sections, gameObject.name);
    }
}
