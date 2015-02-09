using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {
        private int xWins;

        public int XWins
        {
            get { return xWins; }
            set
            {
                xWins = value;
                UpdateUI();
            }
        }

        private int yWins;

        public int YWins
        {
            get { return yWins; }
            set
            {
                yWins = value;
                UpdateUI();
            }
        }

        private int drawGames;

        public int DrawGames
        {
            get { return drawGames; }
            set
            {
                drawGames = value;
                UpdateUI();
            }
        }
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
            var button = new Button { Width = 100, Height = 100 };
            button.Click += OnButtonClick;

            return button;
        }

        private void AddButtonToBoard(Button button)
        {
            flowLayoutPanel1.Controls.Add(button);
        }

        private string currentTurn = "X";

        private string lastTurn;

        public string LastTurn
        {
            get { return lastTurn; }
            set
            {
                lastTurn = value;
                UpdateUI();
            }
        }

        public string CurrentTurn
        {
            get { return currentTurn; }
            set
            {
                currentTurn = value;
                UpdateUI();
            }
        }
        private int turns;
        private Button currentCell;

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
            currentCell = (Button)sender;

            UpdateLogic();
        }

        private void UpdateLogic()
        {
            SwitchTurn();
            CheckConditions();
        }

        private void UpdateUI()
        {
            currentCell.Text = LastTurn;
            currentCell.Enabled = false;
            turn.Text = CurrentTurn;
            label6.Text = DrawGames.ToString();
            label3.Text = XWins.ToString();
            label4.Text = YWins.ToString();
        }

        private void SwitchTurn()
        {
            LastTurn = CurrentTurn;
            if (CurrentTurn == "X")
                CurrentTurn = "O";
            else
                CurrentTurn = "X";
            turns++;
        }

        void CheckConditions()
        {
            CheckRows("X");
            CheckRows("O");
            CheckColumns("X");
            CheckColumns("O");
            CheckFirstDiagonal("X");
            CheckFirstDiagonal("O");
            CheckSecondDiagonal("X");
            CheckSecondDiagonal("O");
            CheckDraw();
        }

        private void CheckDraw()
        {
            if (turns == 9)
            {
                MessageBox.Show("Game Draw");
                DrawGames++;
                ClearBoard();
            }
        }

        private void CheckRows(string who)
        {
            for (int i = 0; i < 3; i++)
            {
                if (RowWins(i, who))
                {
                    Won(who);
                    return;
                }
            }
        }

        private void CheckColumns(string who)
        {
            for (int i = 0; i < 3; i++)
            {
                if (ColumnWins(i, who))
                {
                    Won(who);
                    return;
                }
            }
        }

        private void CheckFirstDiagonal(string who)
        {
            for (int i = 0; i < RowSize * ColumnSize; i+=4)
            {
                if (buttons[i].Text != who)
                {
                    return;
                }
            }
            Won(who);
        }

        private void CheckSecondDiagonal(string who)
        {
            for (int i = 2; i < 7; i += 2)
            {
                if (buttons[i].Text != who)
                {
                    return;
                }
            }
            Won(who);
        }
        
        private void Won(string who)
        {
            MessageBox.Show(string.Format("{0} Wins", who));
            UpdateScore(who);
            ClearBoard();
        }

        private void UpdateScore(string who)
        {
            if (who == "X")
            {
                XWins++;
                return;
            }

            YWins++;
        }

        private const int RowSize = 3;

        private bool RowWins(int row, string who)
        {
            int startWith = row * RowSize;

            for (int i = startWith; i < startWith + RowSize; i++)
            {
                if (buttons[i].Text != who)
                {
                    return false;
                }
            }
            return true;
        }

        const int ColumnSize = 3;

        private bool ColumnWins(int column, string who)
        {
            int startWith = column * RowSize;

            for (int i = column; i < RowSize * ColumnSize; i+= RowSize)
            {
                if (buttons[i].Text != who)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
