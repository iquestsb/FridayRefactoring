using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {
        private int xWins;

        private int yWins;

        private int drawGames;

        Board board = new Board();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
                CreateButton(i);
        }

        private void CreateButton(int i)
        {
            Button button = new Button { Width = 100, Height = 100 };
            button.Tag = i;
            button.Click += OnButtonClick;
            buttonsLayout.Controls.Add(button);
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int index = (int)button.Tag;
            
            board.MarkCell(index);

            UpdateUI();
            
            if (board.IsGameOver())
            {
                DisplayResult();
                UpdateScore();
                board.Clear();
            }

            UpdateUI();
        }

        private void UpdateUI()
        {
            turn.Text = board.CurrentTurn;
            label6.Text = drawGames.ToString();
            label3.Text = xWins.ToString();
            label4.Text = yWins.ToString();

            string[] cells = board.cells;

            for (int i = 0; i < 9; i++)
                UpdateCell(cells[i], i);
        }

        private void UpdateCell(string value, int i)
        {
            buttonsLayout.Controls[i].Text = value;
            buttonsLayout.Controls[i].Enabled = string.IsNullOrEmpty(value);
        }

        private void DisplayResult()
        {
            if (board.IsDraw())
            {
                MessageBox.Show("Draw");
                return;
            }

            MessageBox.Show(String.Format("{0} won", board.LastTurn));
        }

        private void UpdateScore()
        {
            if (board.IsDraw())
            {
                drawGames++;
                return;
            }
            
            if (board.LastTurn == "X")
            {
                xWins++;
                return;
            }

            yWins++;
        }
    }
}
