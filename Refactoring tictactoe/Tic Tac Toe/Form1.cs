using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {
        private int xWins;
        private int yWins;
        private int draws;
        private List<Button> buttons = new List<Button>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateBoard();
        }

        private void CreateBoard()
        {
            for (int i = 0; i < 9; i++)
                CreateCell();
        }

        private void CreateCell()
        {
            Button button = CreateButton();
            buttons.Add(button);
            AddButtonToBoard(button);
        }        

        private Button CreateButton()
        {
            var button = new Button {Width = 100, Height = 100};
            button.Click += OnButtonClick;

            return button;
        }

        private void AddButtonToBoard(Button button)
        {
            flowLayoutPanel1.Width = 350;
            flowLayoutPanel1.Controls.Add(button);
        }

        private string currentTurn = "X";
        private int turns;

        private void ClearBoard()
        {
            for (int i = 0; i < 9; i++)
                ClearCell(i);

            turns = 0;
        }

        private void ClearCell(int i)
        {
            buttons[i].Enabled = true;
            buttons[i].Text = "";
        }


        private void OnButtonClick(object sender, EventArgs e)
        {
            Button cell = (Button)sender;
            cell.Text = currentTurn;

            SwitchTurn();

            turn.Text = currentTurn;
            cell.Enabled = false;

            turns++;

            //call Check funtion for conditions
            Check();
            if (IsDraw())
            {
                draws++;
                UpdateUi();
            }
        }

        private void UpdateUi()
        {
            MessageBox.Show("Game Draw");            
            numberOfDraws.Text = draws.ToString();
            ClearBoard();
        }

        private bool IsDraw()
        {
            const int maxTurns = 9;
            return turns == maxTurns;
        }

        private void SwitchTurn()
        {
            if (currentTurn == "X")
                currentTurn = "O";
            else
                currentTurn = "X";
        }

        private void Check()
        {
            ButtonsMatch(buttons[0], buttons[1], buttons[2]);
            ButtonsMatch(buttons[3], buttons[4], buttons[5]);
            ButtonsMatch(buttons[6], buttons[7], buttons[8]);

            ButtonsMatch(buttons[0], buttons[3], buttons[6]);
            ButtonsMatch(buttons[1], buttons[4], buttons[7]);
            ButtonsMatch(buttons[2], buttons[5], buttons[8]);

            ButtonsMatch(buttons[0], buttons[4], buttons[8]);
            ButtonsMatch(buttons[2], buttons[4], buttons[6]);            
        }

        private bool ButtonsMatch(Button button1, Button button2, Button button3)
        {
            if (button1.Text == "")
            {
                return false;
            }

            if (button1.Text == button2.Text && button2.Text == button3.Text)
            {
                SetWinner(button1);
                ClearBoard();

                return true;
            }

            return false;
        }

        private void SetWinner(Button button1)
        {
            MessageBox.Show(button1.Text + " Wins");
            if (button1.Text == "X")
            {
                xWins++;
                xScore.Text = xWins.ToString();
            }
            else
            {
                yWins++;
                yScore.Text = yWins.ToString();
            }
        }
    }

}
