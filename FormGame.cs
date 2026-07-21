using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace _160425129_Laura_FinderApp
{
    public partial class FormGame : Form
    {
        public FormGame formGame;
        public FormGuardian formGuardian;

        Time time;
        public Players player;

        WindowsMediaPlayer backSoundPlayer = new WindowsMediaPlayer();
        WindowsMediaPlayer otherSoundPlayer;

        public Persons activePerson;
        Point activePersonLastLocation;
        private string playerName = ""; //nama

        bool enterTalkArea = false;

        bool paused = false;

        int numOfWalkArea = 3;
        WalkAreas currentWalkArea = null;
        TalkAreas currentTalkArea = null;

        string difficulty = "Normal"; // menyimpan tingkat kesulitan yang dipilih player
        public bool useHint = false;
        public bool firstQuestionAchievement = false;
        public int areaMasterCount = 0;
        public bool geniusAchievement = false;
        public bool gameOver = false;


        public bool wrongAnswer = false; //Kalo tidak pernah salah jawab

        public FormGame()
        {
            InitializeComponent();
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            textBoxName.BringToFront();
            buttonOk.BringToFront();

            panelGame.Visible = false;
            labelTime.Visible = false;
            panelTalkArea.Visible = false;

            playPauseGameToolStripMenuItem.Enabled = false;
            timerTime.Interval = 1000;

            this.KeyPreview = true;
            this.DoubleBuffered = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Press arrow key to move the player. \nPress Enter to talk with the person." +
                "\nPress y key to answer the question. \nPress Esc to exit the talk area." , "How To Play");
        }



        private void timerTime_Tick(object sender, EventArgs e)
        {
            time.AddWithSecond(-1);
            labelTime.Text = time.DisplayData();

            if (time.Hour == 0 && time.Minute == 0 && time.Second == 0)
            {
                timerTime.Stop();

                backSoundPlayer.controls.stop();
                PlaySound("lose game");
                MessageBox.Show("Game over. Time is up.");
                GameOver();
            }
        }

        private void FormGame_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (player == null) //pengecekan sebelum menambahkan nama
            {
                return;
            }

            int distance = 30;

            if (paused == false)
            {
                if (e.KeyCode == Keys.Right)
                {
                    if (player.Picture.Location.X + player.Picture.Width >= this.Width - 20)
                    {
                        if (currentWalkArea.CheckFinishAllQuestions() == true)
                        {
                            areaMasterCount++;
                            MessageBox.Show("Achievement Unlocked!\nArea " + currentWalkArea.NoArea + " Master");
                            if (currentWalkArea.NoArea < numOfWalkArea)
                            {
                                currentWalkArea.NoArea += 1;
                                GenerateWalkArea();
                            }
                            else
                            {
                                //win game
                                otherSoundPlayer.controls.stop();
                                PlaySound("win game"); // putar sound kemenangan

                                MessageBox.Show("Congratulations. You have mastered every area.");
                                MessageBox.Show("Now, hold on a sec!");

                                // nanti stop win sound
                                otherSoundPlayer.controls.stop();

                                MessageBox.Show(
                                    "A mysterious force is calling you...",
                                    "Secret Ending");

                                // Jika selama MessageBox waktu habis (Game Over),
                                // maka jangan lanjut masuk ke Guardian.
                                // Biarkan musik Lose Game tetap berjalan.
                                if (gameOver == true)
                                {
                                    return;
                                }

                                // gameOver == false atau game belum selesai maka bisa masuk ke Form Guardian
                                FormGuardian formGuardian = new FormGuardian();
                                formGuardian.formGame = this; // kirim FormGame ke Guardian (untuk bool gameOver)
                                formGuardian.ShowDialog();

                                // Setelah keluar dari Guardian, cek lagi apakah game sudah Game Over.
                                // Misalnya timer habis saat Guardian sedang terbuka.
                                // Jika iya, jangan putar Win Sound lagi.
                                if (gameOver == true)
                                {
                                    return;
                                }

                                // Jika player keluar dari Guardian tanpa Game Over,
                                // putar kembali Win Sound lalu akhiri game.
                                PlaySound("win game");

                                MessageBox.Show(
                                    "Congratulations!\n\nYour adventure has come to an end.",
                                    "Game Completed");

                                GameOver();
                            }
                        }

                    }
                    else
                    {
                        player.MoveRight(distance);
                    }
                }
                else if (e.KeyCode == Keys.Left)
                {
                    if (player.Picture.Location.X >= 10)
                        player.MoveLeft(distance);
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (currentWalkArea.CheckTouchPerson(player, out Persons touchPerson) == true)
                    {
                        enterTalkArea = true;
                        activePerson = touchPerson;
                        activePersonLastLocation = activePerson.Picture.Location;
                        EnterTalkArea();
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    ExitTalkArea();
                }
                else if (e.KeyCode == Keys.Y && activePerson.SolvedStatus == false)
                {
                    FormQuestion form = new FormQuestion();
                    form.Owner = this;
                    form.ShowDialog();
                }
                player.DisplayPicture(this);
            }

        }

        private void playPauseGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(playPauseGameToolStripMenuItem.Text == "Pause Game")
            {
                paused = true;
                timerTime.Stop();
                
                playPauseGameToolStripMenuItem.Text = "Play Game";
                backSoundPlayer.controls.pause();
            }
            else
            {
                paused = false;
                timerTime.Start();
                playPauseGameToolStripMenuItem.Text = "Pause Game";
                backSoundPlayer.controls.play();
            }
        }

      

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            difficulty = "Easy";
            MessageBox.Show("Difficulty set to Easy");
            StartGame();
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            difficulty = "Normal";
            MessageBox.Show("Difficulty set to Normal");
            StartGame();
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            difficulty = "Hard";
            MessageBox.Show("Difficulty set to Hard");
            StartGame();
        }

        #region Method

        private void GameOver()
        {
            gameOver = true; //KALO BNR UDH ABIS WKTNYA, GAME SELESAI

            timerTime.Stop();

            // Matikan seluruh backsound yang sedang berjalan
            backSoundPlayer.controls.stop();

            if (otherSoundPlayer != null)
                otherSoundPlayer.controls.stop();

            // Putar musik kalah
            PlaySound("lose game");

            panelGame.Visible = false;
            labelTime.Visible = false;
            startNewGameToolStripMenuItem.Enabled = true;

        }

        private void StartGame()
        {
            if (player != null && player.Picture != null)
            {
                this.Controls.Remove(player.Picture);
                player.Picture.Dispose();
            }
            panelGame.Visible = true;
            labelTime.Visible = true;
            playPauseGameToolStripMenuItem.Enabled = true;
            startNewGameToolStripMenuItem.Enabled = false;

            playPauseGameToolStripMenuItem.Text = "Pause Game";

            if (difficulty == "Easy")
            {
                time = new Time(0, 3, 0);
            }
            else if (difficulty == "Normal")
            {
                time = new Time(0, 2, 0);
            }
            else
            {
                time = new Time(0, 1, 0);
            }
            timerTime.Start();

            if (currentWalkArea != null)
            {
                currentWalkArea.RemoveAllPersons();
            }
            currentWalkArea = null;
            player = new Players(textBoxName.Text, Properties.Resources.player_right, new Size(50, 80), new Point(10, 370), time);

            labelPlayer.Text = player.DisplayData();

            GenerateWalkArea();

            player.DisplayPicture(this);

            PlaySound("walk area");
            paused = false;

        }

        private void startNewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void GenerateWalkArea()
        {
            if (currentWalkArea == null)
            {
                //ciptakan walk area1
                currentWalkArea = new WalkAreas("The Barn", Properties.Resources.walkArea1, 1);

                //tambah person di walk area1
                currentWalkArea.AddPerson(1, "Anna", Properties.Resources.person1, new Size(60, 90), new Point(150, 350), "I have a question for you. Are you ready? \nPress 'y' to continue");
                currentWalkArea.AddPerson(2, "Andy", Properties.Resources.person2, new Size(60, 90), new Point(420, 360), "Can you answer my question? Lets go! \nPress 'y' to continue");
                currentWalkArea.AddPerson(3, "Boby", Properties.Resources.person3, new Size(60, 90), new Point(600, 360), "Just answer my question please. \nPress 'y' to continue");
            }
            else if (currentWalkArea.NoArea == 2)
            {
                //hapus semua person di area sebelumnya
                currentWalkArea.RemoveAllPersons();

                //ciptakan walk area2
                currentWalkArea = new WalkAreas("The Field", Properties.Resources.walkArea2, 2);

                //tambah person di walk area2
                currentWalkArea.AddPerson(4, "Rina", Properties.Resources.person4, new Size(60, 90), new Point(100, 300), "I'm sure you can answer my question... \nPress 'y' to continue");
                currentWalkArea.AddPerson(5, "Tommy", Properties.Resources.person5, new Size(60, 90), new Point(450, 350), "You look so smart. Can you answer this? \nPress 'y' to continue");
            }
            else if (currentWalkArea.NoArea == 3)
            {
                //hapus semua person di area sebelumnya
                currentWalkArea.RemoveAllPersons();

                //ciptakan walk area3
                currentWalkArea = new WalkAreas("The Field", Properties.Resources.walkArea3, 3);

                //tambah person di walk area3
                currentWalkArea.AddPerson(6, "Marie", Properties.Resources.person6, new Size(60, 90), new Point(120, 390), "Answer my question carefully.. \nPress 'y' to continue");
                currentWalkArea.AddPerson(7, "Luke", Properties.Resources.person7, new Size(60, 90), new Point(470, 390), "I have a question for you. \nPress 'y' to continue");
            }

            //tampilkan gambar walk area
            currentWalkArea.DisplayPicture(this);

            //tampilkan semua person di walk area
            currentWalkArea.DisplayPerson(this);

            labelArea.Text = currentWalkArea.DisplayData();

            if (player != null)
                player.Picture.Location = new Point(0, player.Picture.Location.Y);
        }

       
        private void GenerateTalkArea()
        {
            if (activePerson.NoPerson == 1)
            {
                currentTalkArea = new TalkAreas("Anna's House", Properties.Resources.talkArea1, activePerson);
                activePerson.AddQuestion("Solve this math equation : \n x + y = 10 \n If x = 3, them y = ?", "7", "Subtract x value from 10", 100);
            }
            else if (activePerson.NoPerson == 2)
            {
                currentTalkArea = new TalkAreas("Andy's Room", Properties.Resources.talkArea2, activePerson);
                activePerson.AddQuestion("What is the Capital city of Indonesia ?", "Jakarta", "Starts with J", 50);
            }
            else if (activePerson.NoPerson == 3)
            {
                currentTalkArea = new TalkAreas("Bobbyy's Office", Properties.Resources.talkArea3, activePerson);
                activePerson.AddQuestion("I have this patter \n 1   1   2   3   5   8 ... \nWhat is the next number?", "13", "Fibonacci sequence", 200);
            }
            else if (activePerson.NoPerson == 4)
            {
                currentTalkArea = new TalkAreas("Rina's Room", Properties.Resources.talkArea4, activePerson);
                activePerson.AddQuestion("What is the  chemical compound name for sulfuric acid?", "H2SO4", "Contains H, S, and O", 250);
            }
            else if (activePerson.NoPerson == 5)
            {
                currentTalkArea = new TalkAreas("Tommy's Place", Properties.Resources.talkArea5, activePerson);
                activePerson.AddQuestion("Check this C# codes : \n int result = 10/100; \nMessageBox.Show(result); \nWhat is the output of these code?", "0", "Integer division removes decimal", 170);
            }
            else if (activePerson.NoPerson == 6)
            {
                currentTalkArea = new TalkAreas("Marie's Place", Properties.Resources.talkArea6, activePerson);
                activePerson.AddQuestion("A product has selling price of $100 and is discounted 10% off the list price. It also has a shipping free of $5 + " +
                    "\nIf you want to purchase this product, how much will you have to pay?", "95", "Calculate the discount first", 300);
            }
            else if (activePerson.NoPerson == 7)
            {
                currentTalkArea = new TalkAreas("Luke's House", Properties.Resources.talkArea7, activePerson);
                activePerson.AddQuestion("What is the 1st principle(sila ke- 1) of Pancasila?", "Ketuhanan Yang Maha Esa", "Starts with Ketuhanan", 150);
            }
        }

        //skor berdasarkan difficulty level
        public int GetScoreByDifficulty(int baseScore)
        {
            if (difficulty == "Easy")
            {
                return baseScore - 10;
            }
            else if (difficulty == "Hard")
            {
                return baseScore + 10;
            }

            return baseScore; // Normal
        }
        public void EnterTalkArea()
        {
            GenerateTalkArea();

            player.Picture.Visible = false;

            panelTalkArea.BackgroundImage = currentTalkArea.Background;
            panelTalkArea.Visible = true;
            panelTalkArea.BringToFront();

            activePerson.Picture.Size = new Size(200, 300);
            activePerson.Picture.Location = new Point(300, 100);
            activePerson.DisplayPicture(panelTalkArea);

            if (activePerson.SolvedStatus == true)
            {
                activePerson.Dialog = "Congratulation! \nYou have answered my question.";
            }

            activePerson.DisplayDialog(panelTalkArea);

            PlaySound("talk area");
        }
        public void ExitTalkArea()
        {
            player.Picture.Visible = true;
            enterTalkArea = false;

            panelTalkArea.Visible = false;
            activePerson.Picture.Size = new Size(60, 90);
            activePerson.Picture.Location = activePersonLastLocation;
            activePerson.DisplayPicture(this);

            PlaySound("walk area");
        }
        private void PlaySound(string type)
        {
            otherSoundPlayer = new WindowsMediaPlayer();

            if (type == "walk area")
            {
                backSoundPlayer.URL = Application.StartupPath + "\\sound\\BackSoundWalkArea.mp3";
                backSoundPlayer.settings.setMode("loop", true);
            }
            else if (type == "talk area")
            {
                backSoundPlayer.URL = Application.StartupPath + "\\sound\\BackSoundTalkArea.mp3";
                backSoundPlayer.settings.setMode("loop", true);
            }
            else if (type == "lose game")
            {
                backSoundPlayer.URL = Application.StartupPath + "\\sound\\LoseGame.mp3";
                backSoundPlayer.settings.setMode("loop", true);
            }
            else if (type == "win game")
            {
                backSoundPlayer.URL = Application.StartupPath + "\\sound\\WinGame.mp3";
                backSoundPlayer.settings.setMode("loop", true);
            }
            otherSoundPlayer.controls.play();
        }
        #endregion

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text.Trim() == "")
            {
                MessageBox.Show("Please enter your name.");
                return;
            }
            MessageBox.Show("Player name confirmed!\r\nChoose a difficulty to start your adventure.");

            playerName = textBoxName.Text;
            labelPlayer.Text = playerName;
            // Sembunyikan input nama
            textBoxName.Visible = false;
            buttonOk.Visible = false;
            label1.Visible = false;
        }

    
    }
}
