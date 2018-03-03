//LongtengZhang, CIS345, TTH 12PM, Project Beta
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaNow
{
    class QuestionEventArgs : EventArgs
    {
        private Question question;

        public QuestionEventArgs(Question question)
        {
            this.question = question;
        }

        public Question Question
        {
            get
            {
                return question;
            }

            set
            {
                question = value;
            }
        }




    }
}
