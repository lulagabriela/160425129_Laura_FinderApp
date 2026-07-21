using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _160425129_Laura_FinderApp
{
    public partial class FormQuestion : Form
    {
        FormGame formGame;
        bool hintUsed = false;
        public FormQuestion()
        {
            InitializeComponent();
        }

        private void FormQuestion_Load(object sender, EventArgs e)
        {
            formGame = (FormGame)this.Owner;
            labelQuestion.Text = formGame.activePerson.PersonQuestion.Questions;
            buttonHint.Enabled = true;
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            //cek jawaban player apakah sesuai dgn kunci jawaban pertanyaan person
            if (formGame.activePerson.CheckAnswer(textBoxAnswer.Text, out int score) == true)
            {
                score = formGame.GetScoreByDifficulty(score); //nanti isi parameternya
                if (hintUsed == true)
                {
                    score = score - 10;
                }

                MessageBox.Show("Your answer is correct! You get " + score + " points.");
                formGame.player.AddScore(score); //tambah skor player
                if (formGame.firstQuestionAchievement == false) //false = blm dpt achievement, true = sdh dpt
                {
                    formGame.firstQuestionAchievement = true; /*Kalau pemain belum pernah mendapatkan achievement => berikan achievement => tandai sudah pernah dpt achievement = > jangan pernah tampilkan lagi*/

                    MessageBox.Show("Achievement Unlocked!\nFirst Question");
                }
                formGame.labelPlayer.Text = formGame.player.DisplayData(); //tampilkan data player terbaru di label
            }
            else
            {
                formGame.wrongAnswer = true;
                MessageBox.Show("Sorry.. your answer is incorrect!");
            }

            this.Close(); //tutup form question
            formGame.ExitTalkArea(); //keluar dari talk area
        }

        private void buttonHint_Click(object sender, EventArgs e)
        {
            MessageBox.Show(formGame.activePerson.PersonQuestion.Hint, "Hint");
            /*Ambil NPC yang sedang diajak bicara => ambil soal miliknya => ambil hint dari soal tersebut => tampilkan ke MessageBox*/

            hintUsed = true;

            formGame.useHint = true;

            buttonHint.Enabled = false;
        }
    }
}
