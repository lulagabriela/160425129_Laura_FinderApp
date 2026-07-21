using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _160425129_Laura_FinderApp
{
    public class Persons
    {
        private int noPerson;
        private string name;
        private PictureBox picture; //composition
        private string dialog; //composition
        private Question personQuestion; //composition
        private bool solvedStatus;

        public Persons(int noPerson, string name, Image image, Size size, Point location, string dialog)
        {
            this.NoPerson = noPerson;
            this.Name = name;
            this.Picture = new PictureBox();
            this.picture.Image = image;
            this.picture.Location = location;
            this.picture.Size = size;
            this.picture.Location = location;
            Dialog = dialog;
            SolvedStatus = false;
        }

        public int NoPerson 
        {
            get => noPerson;
            set
            {
                if ( value > 0)
                {
                    noPerson = value;
                }
                else
                {
                    throw new Exception("No person can not be empty!");
                }
            }
        }
        public string Name
        {
            get => name; 
            set
            {
                if (value != "")
                {
                    name = value;
                }
                else
                {
                    throw new Exception("Name person can not be empty!");
                }
            }
        }
        public PictureBox Picture { get => picture; set => picture = value; }
        public string Dialog { get => dialog; set => dialog = value; }
        public Question PersonQuestion { get => personQuestion; set => personQuestion = value; }
        public bool SolvedStatus { get => solvedStatus; private set => solvedStatus = value; }

        public void DisplayPicture(Control container)
        {
            this.Picture.Parent = container;
            this.Picture.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Picture.BackColor = Color.Transparent;
            this.Picture.BringToFront();
        }

        public void AddQuestion(string question, string answer, string hint, int score)
        {
            personQuestion = new Question(question, answer, hint, score);
        }

        public string DisplayData()
        {
            string data = "Hi... I'm " + this.Name + ".\n" + this.Dialog;
            return data;
        }
        public void DisplayDialog(Control container)
        {
            Label labelDialog = new Label();
            labelDialog.Parent = container;
            labelDialog.Text = this.DisplayData();
            labelDialog.Font = new Font("Arial", 18);
            labelDialog.TextAlign = ContentAlignment.TopCenter;
            labelDialog.Size = new Size(500, 90);

            //set location agar label dialog di atas person

            labelDialog.Location = new Point(this.Picture.Location.X - 150, 10);
            labelDialog.BackColor = Color.LightYellow;
            labelDialog.BorderStyle= BorderStyle.FixedSingle;
            labelDialog.Visible = true;
            labelDialog.BringToFront();
        }

        public bool CheckAnswer(string playerAnswer, out int score)
        {
            if (playerAnswer.ToLower() == this.PersonQuestion.Answer.ToLower())
            {
                this.SolvedStatus = true;
                score = this.personQuestion.Score;
                return true;
            }
            else
            {
                score = 0;
                return false;
            }
        }
    }
}