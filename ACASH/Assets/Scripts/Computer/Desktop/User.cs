using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New User", menuName = "Computer/User")]
public class User : ScriptableObject
{
    public string password;
    public Sprite background;
    public Icon[] icons;
}
