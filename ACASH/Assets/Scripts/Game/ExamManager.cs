using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using N_BH;

public class ExamManager : Singleton<ExamManager>
{
    public Dictionary<string, bool> banned = new Dictionary<string, bool>()
    {
        { "Pen_", false },
        { "Calculator_", false },
        { "Talking_", false },
        { "Cheating_", false },
        { "Walking_", false },
    };
}
