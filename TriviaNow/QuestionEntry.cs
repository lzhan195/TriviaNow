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
    public delegate void QuestionCreated(object sender, EventArgs e);

    public partial class QuestionEntry : Form
    {
        public event QuestionCreated NewQuestionCreated;

        public QuestionEntry()
        {
            InitializeComponent();
            correctChoiceCombox.SelectedIndex = 0;
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            if (questionTextBox.Text == String.Empty || choiceTextBox1.Text == String.Empty 
                || choiceTextBox2.Text == String.Empty || choiceTextBox3.Text == String.Empty 
                || choiceTextBox4.Text == String.Empty || feedbackTextBox.Text == String.Empty
                || correctChoiceCombox.SelectedIndex < 0)
            {
                MessageBox.Show("Enter all data fields.");
                return;
            }

            // Enclose new data in a question object
            // put student object inside a DataEntryEventArgs object
            Question tmpQuestion = new Question(questionTextBox.Text,choiceTextBox1.Text,
                choiceTextBox2.Text,choiceTextBox3.Text,choiceTextBox4.Text,feedbackTextBox.Text, correctChoiceCombox.SelectedIndex);
            QuestionEventArgs tmpArgs = new QuestionEventArgs(tmpQuestion);

            // raise QuestionCreated event
            // use upcasting and pass tmpArgs as EventArgs
            NewQuestionCreated(this, tmpArgs);

            // reset textboxes for entry
            questionTextBox.Clear();
            choiceTextBox1.Clear();
            choiceTextBox2.Clear();
            choiceTextBox3.Clear();
            choiceTextBox4.Clear();
            feedbackTextBox.Clear();
            correctChoiceCombox.SelectedIndex = 0;
            questionTextBox.Focus();
        }
    }

}
