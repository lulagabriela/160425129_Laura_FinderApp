using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _160425129_Laura_FinderApp
{
    public class Question
    {
        #region Data Member
        private string question;
        private string answer;
        private int score;
        private string hint;
        #endregion

        #region Constructor
        public Question(string question, string answer, string hint, int score)
        {
            this.Questions = question;
            this.Answer = answer;
            this.Score = score;
            this.Hint = hint;
        }
        #endregion

        #region Property
        public string Questions
        {
            get => question;
            set
            {
                if (value != "")
                {
                    question = value;
                }
                else
                {
                    throw new Exception("Question cannot be empty");
                }

            }

        }
        public string Answer
        {
            get => answer;
            set
            {
                if (value != "")
                {
                    answer = value;
                }
                else
                {
                    throw new Exception("Answer cannot be empty");
                }
            }
        }
        public int Score
        {
            get => score;
            set
            {
                if (value != 0)
                {
                    score = value;
                }
                else
                {
                    score = 0;
                }
            }
        }

        public string Hint { get => hint; set => hint = value; }
        #endregion
    }
}