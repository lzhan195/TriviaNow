//LongtengZhang, CIS345, TTH 12PM, Project Beta
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaNow
{
    [Serializable]
    public class Question
    {
        private string questionText;
        private List<string> choices;
        private string feedbackText;
        private int correctChoice;

        public Question()
        {
            Choices = new List <string>();
        }

        public Question(string questionText, string answerChoice1, string answerChoice2, string answerChoice3, string answerChoice4, string feedbackText, int correctChoice)
        {
            this.QuestionText = questionText;
            this.Choices = new List<string>() { answerChoice1, answerChoice2, answerChoice3, answerChoice4};
            this.FeedbackText = feedbackText;
            this.CorrectChoice = correctChoice;
        }

        public string QuestionText { get => questionText; set => questionText = value; }
        public string FeedbackText { get => feedbackText; set => feedbackText = value; }
        public int CorrectChoice { get => correctChoice; set => correctChoice = value; }
        public List<string> Choices { get => choices; set => choices = value; }

        public override string ToString()
        {
            return questionText;
        }
    }
}
