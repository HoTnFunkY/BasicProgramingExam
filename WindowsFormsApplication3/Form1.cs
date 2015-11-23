using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Yatzy : Form
    {
        private int numberOfPlayers = 0;
        private Font textFont = new Font("Arial", 9);
        private string[] playerNames = new string[5]; 
        Form players = new Form();
        Form addPlayer = new Form();
        TextBox numberOfPlayerTextBox = new TextBox();
        TextBox addPlayerTextBox = new TextBox();

        public Yatzy()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Green;
            this.Location = new Point(300, 100);
            this.MaximizeBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numberOfPlayersForm();            
        }

        private void numberOfPlayersForm()
        {
            players.Size = new Size(250, 250);
            players.StartPosition = FormStartPosition.CenterParent;
            players.MaximizeBox = false;

            Label numberOfPlayersLabel = new Label();
            numberOfPlayersLabel.Location = new Point(10, 25);
            numberOfPlayersLabel.AutoSize = true;
            numberOfPlayersLabel.Text = "Hvor mange spillere vil i spille? 1- 4";
            numberOfPlayersLabel.Font = textFont;

            //TextBox numberOfPlayerTextBox = new TextBox();
            numberOfPlayerTextBox.Location = new Point(98, 80);
            numberOfPlayerTextBox.Size = new Size(40, 20);
            numberOfPlayerTextBox.MaxLength = 1;
            numberOfPlayerTextBox.TextAlign = HorizontalAlignment.Center;
            numberOfPlayerTextBox.Font = textFont;
            numberOfPlayerTextBox.KeyPress += new KeyPressEventHandler(numberOfPlayerTextBox_KeyPress);

            Button numberOfPlayerButton = new Button();
            numberOfPlayerButton.Location = new Point(80, 120);
            numberOfPlayerButton.Text = "Ok";
            numberOfPlayerButton.Font = textFont;
            numberOfPlayerButton.Click += new EventHandler(numberOfPlayerButton_Click);

            players.Controls.Add(numberOfPlayersLabel);
            players.Controls.Add(numberOfPlayerTextBox);
            players.Controls.Add(numberOfPlayerButton);
            players.ShowDialog();
        }

        private void addPlayersForm(string label, int playerNumber)
        {
            Form addPlayer = new Form();
            addPlayer.Size = new Size(250, 250);
            addPlayer.StartPosition = FormStartPosition.CenterParent;
            addPlayer.MaximizeBox = false;

            Label addPlayerLabel = new Label();
            addPlayerLabel.Location = new Point(10, 25);
            addPlayerLabel.AutoSize = true;
            addPlayerLabel.Text = label;
            addPlayerLabel.Font = textFont;

            //TextBox addPlayerTextBox = new TextBox();
            addPlayerTextBox.Location = new Point(78, 80);
            addPlayerTextBox.Size = new Size(80, 20);
            addPlayerTextBox.MaxLength = 10;
            addPlayerTextBox.TextAlign = HorizontalAlignment.Center;
            addPlayerTextBox.Font = textFont;
            //addPlayerTextBox.KeyPress += new KeyPressEventHandler(numberOfPlayerTextBox_KeyPress);

            Button addPlayerButton = new Button();
            addPlayerButton.Location = new Point(80, 120);
            addPlayerButton.Text = "Tilføj";
            addPlayerButton.Font = textFont;
            addPlayerButton.Click += new EventHandler((s, e) => addPlayerButton_Click(s, e, playerNumber));

            addPlayer.Controls.Add(addPlayerLabel);
            addPlayer.Controls.Add(addPlayerTextBox);
            addPlayer.Controls.Add(addPlayerButton);
            addPlayer.ShowDialog();
        }

        private void numberOfPlayerButton_Click(object sender, EventArgs e)
        {
            if (numberOfPlayerTextBox.Text != "")
            {
                numberOfPlayers = int.Parse(numberOfPlayerTextBox.Text);
                for (int i = 1; i <= numberOfPlayers; i++)
                {
                    addPlayersForm("Skriv brugernavn på spiller " + i + "\n eller PC for computer", i);
                    //MessageBox.Show(addPlayerTextBox.Text);
                }
                players.Close();
                scoreboardList();
            }
        }

        private void addPlayerButton_Click(object sender, EventArgs e, int playerNumber)
        {
            if (addPlayerTextBox.Text != "")
            {
                playerNames[playerNumber] = addPlayerTextBox.Text;
                addPlayer.DialogResult = DialogResult.OK;
                addPlayer.Close();
            }
        }

        private void scoreboardList()
        {
            ListView scoreboard = new ListView();
            scoreboard.Bounds = new Rectangle(new Point(400, 100), new Size(84 + (60 * numberOfPlayers), 400));
            scoreboard.View = View.Details;
            scoreboard.GridLines = true;
            scoreboard.FullRowSelect = true;

            string[] info = new string[18];
            string[] row = new string[numberOfPlayers + 1];
            info[0] = "1'ere";
            info[1] = "2'ere";
            info[2] = "3'ere";
            info[3] = "4'ere";
            info[4] = "5'ere";
            info[5] = "6'ere";
            info[6] = "sum";
            info[7] = "bonus";
            info[8] = "Et par";
            info[9] = "To par";
            info[10] = "Tre ens";
            info[11] = "Fire ens";
            info[12] = "Lav";
            info[13] = "Høj";
            info[14] = "Fuldt hus";
            info[15] = "Chance";
            info[16] = "Yatzy";
            info[17] = "I alt";
            scoreboard.Columns.Add("Spillere", 80, HorizontalAlignment.Left);
            for (int i = 1; i <= numberOfPlayers; i++)
            {
                scoreboard.Columns.Add(playerNames[i], 60, HorizontalAlignment.Left);
            }

            ListViewItem itm;
            for (int i = 0; i <= 17; i++)
            {
                row[0] = info[i];
                for (int k = 1; k <= numberOfPlayers; k++)
                {
                    row[k] = "-";
                }
                itm = new ListViewItem(row);
                scoreboard.Items.Add(itm);
            }
            scoreboard.Items[1].SubItems[1].Text = "Hej";
            scoreboard.Font = textFont;
            this.Controls.Add(scoreboard);
            scoreboard.BringToFront();
        }

        private void numberOfPlayerTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if ((ch < '1' || ch > '4') && ch != 8)
            {
                e.Handled = true;
                MessageBox.Show("Skriv et tal mellem 1-4");
            }            
        }


    }
}
