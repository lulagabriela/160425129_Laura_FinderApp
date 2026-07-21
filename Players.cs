using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _160425129_Laura_FinderApp
{ 
    public class Players
    {
        private string name;
        private PictureBox picture;
        private int score;
        private Time playTime;

        public Players(string name, Image image, Size size, Point location, Time playTime)
        {
            this.Name = name;
            this.Picture = new PictureBox();
            this.Score = 0;
            this.PlayTime = playTime;
            this.Picture.Image = image;
            this.Picture.Location = location;
            this.Picture.Size = size;
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
                    throw new Exception("Name can not be empty!");
                }
            }
        }

        public PictureBox Picture { get => picture; set => picture = value; }
        public int Score
        {
            get => score;
            private set
            {
                if (value >= 0)
                {
                    score = value;
                }
                else
                {
                    score = 0;
                }
            }
        }

        public Time PlayTime { get => playTime; set => playTime = value; }

        #region methods
        public string DisplayData()
        {
            string data = "Name : " + this.Name +
                "\nScore : " + this.Score +
                "\nPlaytime : " + this.PlayTime.DisplayData();
            return data;
        }

        public void DisplayPicture(Control container)
        {
            this.Picture.Parent = container;
            this.Picture.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Picture.BackColor = Color.Transparent;
            this.Picture.BringToFront();
        }

        public void MoveRight(int distance)
        {
            this.Picture.Location = new Point(this.Picture.Location.X + distance, this.Picture.Location.Y);
            this.Picture.Image = Properties.Resources.player_right;
        }
        public void MoveLeft(int distance)
        {
            this.Picture.Location = new Point(this.Picture.Location.X - distance, this.Picture.Location.Y);
            this.Picture.Image = Properties.Resources.player_right;
        }

        public void AddScore(int score)
        { 
            this.Score += score; 
        }
        #endregion
    }
}
