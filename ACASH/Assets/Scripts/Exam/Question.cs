using UnityEngine;


namespace Exam
{
    [CreateAssetMenu(fileName = "New Question", menuName = "Exam/Question")]
    public class Question : ScriptableObject
    {
        public string question;
        public string[] answers = new string[4];
        [Range(0, 3)]
        public int rightAnswer;
    }
}
