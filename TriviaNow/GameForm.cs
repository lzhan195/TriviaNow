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
using System.Media;

namespace TriviaNow
{
    public partial class GameForm : Form
    {
        SoundPlayer sound = new SoundPlayer("s2.wav");
        BindingList<Question> questions;
        BindingList<Question> quizQestions;
        int score = 0;
        int questionNumber = 0;
        bool choiceSelected = false;

        public GameForm()
        {
            InitializeComponent();
            questions = new BindingList<Question>();
            quizQestions = new BindingList<Question>();
        }

        public GameForm(BindingList<Question> questions)
        {
            InitializeComponent();
            this.questions = questions;
            quizQestions = new BindingList<Question>();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            finishPanel.Visible = false;
            GenerateQuizQuestions();
            ShowQuestion();
            sound.PlayLooping();
        }

        
        private void ShowQuestion()
        {
            //select the current question to show and its details
            Question tmpQuestion = quizQestions[questionNumber];
            questionTextLabel.Text = tmpQuestion.QuestionText;
            choiceLabel1.Text = tmpQuestion.Choices[0];
            choiceLabel2.Text = tmpQuestion.Choices[1];
            choiceLabel3.Text = tmpQuestion.Choices[2];
            choiceLabel4.Text = tmpQuestion.Choices[3];
            feedbackLabel.Text = tmpQuestion.FeedbackText;
            feedbackLabel.Visible = false;
            choiceSelected = false;
        }

        private void GenerateQuizQuestions()
        {
            //generate three unique questions for the quiz
            Random tmpRandom = new Random();
            quizQestions = new BindingList<Question>();
            for (int i = 0; i < 3;)
            {
                int index = tmpRandom.Next(0, questions.Count);
                Question tmpQuestion = questions[index];
                if (!quizQestions.Contains(tmpQuestion))
                {
                    quizQestions.Add(tmpQuestion);
                    i++;
                }
            }
        }

        // keep the program moving until answering all three questions
        private void nextButton_Click(object sender, EventArgs e)
        {
            choiceLabel1.BackColor = Color.RoyalBlue;
            choiceLabel2.BackColor = Color.RoyalBlue;
            choiceLabel3.BackColor = Color.RoyalBlue;
            choiceLabel4.BackColor = Color.RoyalBlue;
            questionNumber++;
            if (questionNumber == 2)
            {
                nextButton.Text = "Finish";
            }

            if (questionNumber <= 2)
            {
                ShowQuestion();
                questionCountLabel.Text = $"{questionNumber + 1} / {quizQestions.Count}";
                scoreLabel.Text = $"Score: {score}";
            }
            else
            {
                finishPanel.Visible = true;
                finalScoreLabel.Text = $"Score: {score}";
            }
            choiceSelected = false;
        }

        private void choiceLabel_Click(object sender, EventArgs e)
        {
            if (choiceSelected) return;
            choiceSelected = true;
            Label tmpLabel = sender as Label;

            Question currentQuestion = quizQestions[questionNumber];
            string answer = currentQuestion.Choices[currentQuestion.CorrectChoice];
            if (tmpLabel.Text == answer)
            {
                tmpLabel.BackColor = Color.Green;
                score += 1;
            }
            else
            {
                tmpLabel.BackColor = Color.Red;
                switch (currentQuestion.CorrectChoice)
                {
                    case 0:
                        choiceLabel1.BackColor = Color.Green;
                        break;
                    case 1:
                        choiceLabel2.BackColor = Color.Green;
                        break;
                    case 2:
                        choiceLabel3.BackColor = Color.Green;
                        break;
                    case 3:
                        choiceLabel4.BackColor = Color.Green;
                        break;
                }
            }
            scoreLabel.Text = $"Score: {score}";
            feedbackLabel.Visible = true;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            sound.Stop();
            this.Close();
        }
    }
}
