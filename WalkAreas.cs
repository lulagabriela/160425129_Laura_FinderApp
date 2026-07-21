using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace _160425129_Laura_FinderApp
{
    public class WalkAreas : Areas
    {
        private int noArea;
        private List<Persons> listPersons;

        public WalkAreas(string name, Image background, int noArea) : base(name, background)
        {
            this.NoArea = noArea;
            this.ListPersons = new List<Persons>();
        }

        public int NoArea { get => noArea; set => noArea = value; }
        public List<Persons> ListPersons { get => listPersons; set => listPersons = value; }

        public override string DisplayData()
        {
            return "No. Area : " + this.NoArea + " - " + base.DisplayData();
        }

        public void AddPerson(int no, string name, Image image, Size size, Point location, string dialog)
        {
            Persons person = new Persons(no, name, image, size, location, dialog);
            this.ListPersons.Add(person);
        }

        public void DisplayPerson(Control container)
        {
            foreach (Persons person in this.ListPersons)
            {
                person.DisplayPicture(container);
            }
        }

        public void RemoveAllPersons()
        {
            foreach (Persons person in this.ListPersons)
            {
                person.Picture.Dispose();
            }
            this.listPersons.Clear();
        }

        public bool CheckTouchPerson(Players player, out Persons touchPerson)
        {
            foreach (Persons person in this.listPersons)
            {
                if (player.Picture.Bounds.IntersectsWith(person.Picture.Bounds))
                {
                    touchPerson = person;
                    return true;
                }
            }
            touchPerson = null;
            return false;
        }

        public bool CheckFinishAllQuestions()
        {
            int numSolved = 0;
            foreach (Persons person in this.listPersons)
            {
                if (person.SolvedStatus == true)
                {
                    numSolved++;
                }
            }

            if (numSolved == this.ListPersons.Count) // jika jumlah solved sama dgn jmlh person di lis
                return true;
            else
                return false;
        }
    }
}