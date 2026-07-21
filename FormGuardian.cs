using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace _160425129_Laura_FinderApp
{
    public partial class FormGuardian : Form
    {
        public FormGuardian()
        {
            InitializeComponent();
        }

        public FormGame formGame;
        WindowsMediaPlayer backSoundPlayer = new WindowsMediaPlayer();
        bool readyForQuestion = false;

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (textBoxAnswer.Text == "45")
            {
                MessageBox.Show(
                    "Congratulations, young adventurer.\n\n" +
                    "You have proven yourself worthy.\n\n" +
                    "For centuries, I have protected this sacred temple.\n\n" +
                    "But now my duty comes to an end.\n\n" +
                    "You are the next Ancient Guardian.\n\n" +
                    "TRUE ENDING UNLOCKED!",
                    "The Next Guardian");

                backSoundPlayer.controls.stop();
                Application.Exit();
            }
            else
            {
                MessageBox.Show(
                    "You are not yet worthy.\n\n" +
                    "Return when your wisdom has grown.",
                    "Ancient Guardian");

                textBoxAnswer.Clear();
                textBoxAnswer.Focus();
            }
        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            backSoundPlayer.controls.stop();

            MessageBox.Show(
                "You decided to leave the Ancient Guardian.\n\nThe secret remains hidden.",
                "Ending");

            this.Close();
        }


        Timer timerCheck = new Timer();
        private void FormGuardian_Load(object sender, EventArgs e)
        {
            timerCheck.Interval = 1000; // sama seperti timer game (1 detik)
            timerCheck.Tick += timerCheck_Tick;
            timerCheck.Start();

            if (formGame.gameOver)
            {
                backSoundPlayer.controls.stop();
                this.Close();
            }

            backSoundPlayer.URL = Application.StartupPath + "\\sound\\GuardianBacksound.mp3";

            backSoundPlayer.settings.setMode("loop", true);

            backSoundPlayer.controls.play();

            labelQuestion.Visible = false;
            textBoxAnswer.Visible = false;
            btnSubmit.Visible = false;
            btnLeave.Visible = false;

            labelGuardianDialog.Text =
            "Greetings, young adventurer...\n\n" +
            "I am the Ancient Guardian,\n" +
            "protector of this sacred temple.\n\n" +
            "You have conquered every challenge.\n\n" +
            "One final trial remains." +
            "\nPress Y to begin the final trial.";


            labelQuestion.Text = "Young adventurer,\n\n" +
            "What is the decimal equivalent\n" +
            "of binary 101101?";
        }

        private void FormGuardian_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Y && readyForQuestion == false)
            {
                readyForQuestion = true;
                MessageBox.Show(
                    "The final trial begins...",
                    "Ancient Guardian");

                labelQuestion.Visible = true;
                textBoxAnswer.Visible = true;
                btnSubmit.Visible = true;
                btnLeave.Visible = true;
            }
        }

        private void timerCheck_Tick(object sender, EventArgs e)
        {
            if (formGame != null && formGame.gameOver) //Kalau udah dibuat object formgame dan gameOver (nilai variabel dari formgame) 
            {
                timerCheck.Stop();              // hentikan timer pengecekan
                backSoundPlayer.controls.stop(); // matikan backsound Guardian
                this.Close();                    // tutup Form Guardian
            }
        }
        private void FormGuardian_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Matikan backsound Guardian saat form ditutup
            backSoundPlayer.controls.stop();
        }
    }
}
