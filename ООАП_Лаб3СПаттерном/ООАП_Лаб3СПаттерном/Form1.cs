using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace ООАП_Лаб3СПаттерном
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            game = new GameHistory();
            Grid.RowCount++;
            for (int i = 0; i < size_field - 1; i++)
            {
                Grid.RowCount++;
                Grid.ColumnCount++;
                Grid.Columns[i].Width = 29;
            }
            Grid.Columns[size_field - 1].Width = 29;

            Init();
        }
        public void Init()
        {
            s = new Play();

            for (int i = 0; i < size_field; i++)
            {
                for (int j = 0; j < size_field; j++)
                {
                    Grid.Rows[i].Cells[j].Style.BackColor = Color.White;
                    mines[i, j] = 0;
                    Grid.Rows[i].Cells[j].Value = null;
                }
            }

            int sup_count_mines = 0;
            int x_cord, y_cord;

            while (sup_count_mines < count_mines)
            {
                x_cord = rand.Next(0, size_field);
                y_cord = rand.Next(0, size_field);
                if (mines[x_cord, y_cord] == 0)
                {
                    mines[x_cord, y_cord] = -1;
                    Grid.Rows[x_cord].Cells[y_cord].Style.BackColor = Color.DarkRed;
                    sup_count_mines++;
                }
            }
            s.DopMines(mines);
            timer1.Start();
        }


        Random rand = new Random();
        const int size_field = 10;
        const int count_mines = 15;
        int[,] mines = new int[size_field, size_field];
        int c = 0;
        Play s;
        GameHistory game;

        class Play
        {
            private int[,] mines; 
            private int scores = 0;

            public void DopMines(int[,] m)
            {
                mines = m;
            }

            public void DopScores(int n)
            {
                scores += n;
            }

            public int GiveScores()
            {
                return scores;
            }

            public Memento SaveState()
            {
                return new Memento(mines, scores);
            }

            public void RestoreState(Memento memento)
            {
                this.mines = memento.Mines;
                this.scores = memento.Scores;
            }
        }

        class Memento
        {
            public int[,] Mines { get; private set; }
            public int Scores { get; private set; }

            public Memento(int[,] mines, int scores)
            {
                this.Mines = mines;
                this.Scores = scores;
            }
        }

        class GameHistory
        {
            public List<Memento> History { get; private set; }
            public GameHistory()
            {
                History = new List<Memento>();
            }
        }

        //class Originator
        //{
        //    private int[,] mines;
        //    private int scores = 0;

        //    public Memento SaveState()
        //    {
        //        return new Memento(mines, scores);
        //    }

        //    public void RestoreState(Memento memento)
        //    {
        //        this.mines = memento.Mines;
        //        this.scores = memento.Scores;
        //    }
        //}

        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!timer1.Enabled) return;

            if (mines[e.RowIndex, e.ColumnIndex] == -1)
            {
                int demine = rand.Next(0, 6);
                if (demine < 3)
                {
                    c++;
                    game.History.Add(s.SaveState());
                    listBox1.Items.Insert(c - 1, "Игра " + c);
                    MessageBox.Show(
                    "Вы проиграли",
                    "Сообщение",
                    MessageBoxButtons.OK);
                    timer1.Stop();
                }
                else
                {
                    s.DopScores(10);
                    Grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.DarkRed;
                }
            }
            else
            {
                s.DopScores(2);
                int k = 0;
                if (e.RowIndex == 0 && e.ColumnIndex == 0)
                {
                    for (int x = e.RowIndex; x <= e.RowIndex + 1; x++)
                    {
                        for (int y = e.ColumnIndex; y <= e.ColumnIndex + 1; y++)
                        {
                            if (mines[x, y] == -1) k++;
                        }
                    }
                }

                if (e.RowIndex == 0 && e.ColumnIndex == size_field - 1)
                {
                    for (int x = e.RowIndex; x <= e.RowIndex + 1; x++)
                    {
                        for (int y = e.ColumnIndex - 1; y <= e.ColumnIndex; y++)
                        {
                            if (mines[x, y] == -1) k++;
                        }
                    }
                }

                if (e.RowIndex == size_field - 1 && e.ColumnIndex == 0)
                {
                    for (int x = e.RowIndex - 1; x <= e.RowIndex; x++)
                    {
                        for (int y = e.ColumnIndex; y <= e.ColumnIndex + 1; y++)
                        {
                            if (mines[x, y] == -1) k++;
                        }
                    }
                }
                if (e.RowIndex == size_field - 1 && e.ColumnIndex == size_field - 1)
                {
                    for (int x = e.RowIndex - 1; x <= e.RowIndex; x++)
                    {
                        for (int y = e.ColumnIndex - 1; y <= e.ColumnIndex; y++)
                        {
                            if (mines[x, y] == -1) k++;
                        }
                    }
                }
                if (e.RowIndex == 0 && e.ColumnIndex < size_field - 1 && e.ColumnIndex > 0)
                {
                    for (int x = e.RowIndex; x <= e.RowIndex + 1; x++)
                    {
                        for (int y = e.ColumnIndex - 1; y <= e.ColumnIndex + 1; y++)
                        {
                            if (mines[x, y] == -1) k++;
                        }
                    }
                }
                if (e.RowIndex == size_field - 1 && e.ColumnIndex < size_field - 1 && e.ColumnIndex > 0)
                {
                    for (int x = e.RowIndex - 1; x <= e.RowIndex; x++)
                    {
                        for (int y = e.ColumnIndex - 1; y <= e.ColumnIndex + 1; y++)
                        {
                            if (mines[x, y] == -1) k++;
                        }
                    }
                }

                if (e.RowIndex > 0 && e.RowIndex < size_field - 1 && e.ColumnIndex == 0)
                {
                    for (int x = e.RowIndex - 1; x <= e.RowIndex + 1; x++)
                    {
                        for (int y = e.ColumnIndex; y <= e.ColumnIndex + 1; y++)
                        {
                            if (mines[x, y] == -1) k++;
                        }
                    }
                }

                if (e.RowIndex > 0 && e.RowIndex < size_field - 1 && e.ColumnIndex == size_field - 1)
                {
                    for (int x = e.RowIndex - 1; x <= e.RowIndex + 1; x++)
                    {
                        for (int y = e.ColumnIndex - 1; y <= e.ColumnIndex; y++)
                        {
                            if (mines[x, y] == -1) k++;
                        }
                    }
                }

                if (e.RowIndex > 0 && e.RowIndex < size_field - 1 && e.ColumnIndex < size_field - 1 && e.ColumnIndex > 0)
                {
                    for (int x = e.RowIndex - 1; x <= e.RowIndex + 1; x++)
                    {
                        for (int y = e.ColumnIndex - 1; y <= e.ColumnIndex + 1; y++)
                        {
                            if (mines[x, y] == -1) k++;
                        }
                    }
                }

                Grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = k;
            }
            label2.Text = s.GiveScores().ToString();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            s.RestoreState(game.History[listBox1.SelectedIndex]);
            label2.Text = s.GiveScores().ToString();
            for (int i = 0; i < size_field; i++)
            {
                for (int j = 0; j < size_field; j++)
                {
                    Grid.Rows[i].Cells[j].Value = mines[i, j];
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Init();
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            s.DopScores(-1);
            label2.Text = s.GiveScores().ToString();
        }
    }
}

