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
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace TriviaNow
{
    public partial class MainForm : Form
    {
        Question selectedQuestion;
        QuestionEntry questionForm;
        EditQuestion editForm;
        GameForm gameForm;
        BindingList<Question> questionList;

        public MainForm()
        {
            InitializeComponent();
        }

        private void StudentForm_NewStudentCreated(object sender, EventArgs e)
        {
            // event handler for NewStudentCreated method
            // The EventArgs object is actually a DataEntryEventArgs object
            // Downcast it and store it in a variable of type DataEntryEventArgs
            QuestionEventArgs tmpArgs = null;

            if (e is QuestionEventArgs)
            {
                // get student out of e object and add to list
                tmpArgs = (QuestionEventArgs)e;
                Question tmpQuestion = tmpArgs.Question;

                questionList.Add(tmpQuestion);

                statusLabel.Text = $"New Question Added - {tmpQuestion.QuestionText}";
                timer.Enabled = true;

            }

        }

        private void EditForm_QuestionUpdated(object sender, EventArgs e)
        {
            if (e is QuestionEventArgs)
            {
                // extract question out of e object and update status
                QuestionEventArgs tmpArgs = (QuestionEventArgs)e;
                Question tmpQuestion = tmpArgs.Question;
                statusLabel.Text = $"Question Updated - {tmpQuestion.QuestionText}";
                timer.Enabled = true;

                // reset the bindings so that the listbox updates
                // manual refresh
                questionList.ResetBindings();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            questionList = new BindingList<Question>();
            questionListBox.DataSource = questionList;
            saveToolStripMenuItem.Enabled = false;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            statusLabel.Text = "Ready";
        }

        private void addQuestionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // instantiate a new data entry form
            questionForm = new QuestionEntry();

            // show the form
            questionForm.Show();

            // set eventhandler for NewQuestionCreated Event
            questionForm.NewQuestionCreated += new QuestionCreated(this.StudentForm_NewStudentCreated);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "data.dat";
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                questionList = binaryFormatter.Deserialize(fs) as BindingList<Question>;
                questionListBox.DataSource = null;
                questionListBox.DataSource = questionList;
                fs.Close();
                statusLabel.Text = "File opened";
                saveToolStripMenuItem.Enabled = true;
            }

            catch(Exception)
            {
                statusLabel.Text = "File cannot be opened!";
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void SaveFile()
        {
            try
            {
                string fileName = "data.dat";
                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fs, questionList);
                fs.Close();
                statusLabel.Text = "File saved";
            }

            catch (Exception)
            {
                statusLabel.Text = "File cannot be saved!";
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
            System.Environment.Exit(0);
        }

        private void editQuestionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (questionListBox.SelectedItem == null)
            {
                MessageBox.Show("Select question for editing.");
                return;
            }
            selectedQuestion = (Question)questionListBox.SelectedItem;

            editForm = new EditQuestion();
            editForm.Show();
            editForm.PopulateData(selectedQuestion);
            editForm.QuestionUpdated += new EventHandler(this.EditForm_QuestionUpdated);
        }

        private void deleteQuestionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (questionListBox.SelectedItem == null)
            {
                MessageBox.Show("Select a question to delete.");
                return;
            }
            selectedQuestion = (Question)questionListBox.SelectedItem;
            questionList.Remove(selectedQuestion);
        }

        private void questionListBox_DoubleClick(object sender, EventArgs e)
        {
            if (questionListBox.SelectedItem == null)
                return;

            // get the selected question from the list box
            Question selectedQuestion = (Question)questionListBox.SelectedItem;

            // create a new edit form
            editForm = new EditQuestion();

            // show the form and set it to show details of the selected question
            //Wire the event handler
            editForm.Show();
            editForm.PopulateData(selectedQuestion);
            editForm.QuestionUpdated += new EventHandler(this.EditForm_QuestionUpdated);
        }

        //open search box
        private void searchQuestionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (questionList == null || questionList.Count == 0)
            {
                MessageBox.Show("Load question before you search the question!");
                return;
            }

            searchTextBox.Visible = true;
            statusLabel.Text = "Question Searching...";
        }

        private void startGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(questionList == null || questionList.Count == 0)
            {
                MessageBox.Show("Load question before you start the game!");
                return;
            }

            gameForm = new GameForm(questionList);
            gameForm.Show();
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            //Search all Questions’ text and display a list of all matching Questions
            string searchText = searchTextBox.Text;
            questionListBox.DataSource = null;
            if ((searchText == string.Empty) || searchText.Equals("Search..."))
            {
                questionListBox.DataSource = questionList;
            }
            else
            {
                BindingList<Question> searchList = new BindingList<Question>();
                foreach (Question question in questionList)
                {
                    if (question.QuestionText.ToUpper().Contains(searchText.ToUpper())
                        || question.Choices.Any(x => x.ToUpper().Contains(searchText.ToUpper()))
                        || question.FeedbackText.ToUpper().Contains(searchText.ToUpper()))
                    {
                        searchList.Add(question);
                    }
                }
                questionListBox.DataSource = searchList;
            }
        }
    }
}
