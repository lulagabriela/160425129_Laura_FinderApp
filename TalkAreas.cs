using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace _160425129_Laura_FinderApp
{
    public class TalkAreas : Areas
    {
        private Persons person;

        public TalkAreas(string name, Image background, Persons person) : base(name, background)
        {
            Person = person;
        }

        public Persons Person { get => person; set => person = value; }

        public override string DisplayData()
        {
            return this.person.Name +"'s Talk Area - "+ base.DisplayData();
        }
    }
}