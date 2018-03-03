//LongtengZhang, CIS345, TTH 12PM, Project Final
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TriviaNow
{
    public partial class EditQuestion : Form
    {
        public event EventHandler QuestionUpdated;
        private Question currentQuestion;

        public EditQuestion()
        {
            InitializeComponent();
        }


        private void updateButton_Click(object sender, EventArgs e)
        {
            Question tmpQuestion = new Question(questionTextBox.Text, choiceTextBox1.Text,
                choiceTextBox2.Text, choiceTextBox3.Text, choiceTextBox4.Text, feedbackTextBox.Text, correctChoiceCombox.SelectedIndex);

            currentQuestion.QuestionText = questionTextBox.Text;
            currentQuestion.Choices[0] = choiceTextBox1.Text;
            currentQuestion.Choices[1] = choiceTextBox2.Text;
            currentQuestion.Choices[2] = choiceTextBox3.Text;
            currentQuestion.Choices[3] = choiceTextBox4.Text;
            currentQuestion.FeedbackText = feedbackTextBox.Text;
            currentQuestion.CorrectChoice = correctChoiceCombox.SelectedIndex;


            QuestionEventArgs tmpArgs = new QuestionEventArgs(tmpQuestion);

            if (QuestionUpdated != null)
                QuestionUpdated(this, tmpArgs);

            this.Close();
        }

        //show the question details in the form
        public void PopulateData(Question question)
        {
            currentQuestion = question;

            questionTextBox.Text = currentQuestion.QuestionText;
            choiceTextBox1.Text = currentQuestion.Choices[0];
            choiceTextBox2.Text = currentQuestion.Choices[1];
            choiceTextBox3.Text = currentQuestion.Choices[2];
            choiceTextBox4.Text = currentQuestion.Choices[3];
            feedbackTextBox.Text = currentQuestion.FeedbackText;
            correctChoiceCombox.SelectedIndex = question.CorrectChoice;

        }
    }
}
