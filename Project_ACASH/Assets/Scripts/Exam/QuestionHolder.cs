using Single;

namespace Exam
{
    public class QuestionHolder : Singleton<QuestionHolder>
    {
        public Question[] questions = new Question[3];
    }
}
