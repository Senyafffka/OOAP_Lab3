using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ООАП_Лаб3GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
            scores = 0;
            for (int i = 0; i < size_field; i++)
            {
                for (int j = 0; j < size_field; j++)
                {
                    Grid.Rows[i].Cells[j].Style.BackColor = Color.White;
                    mines[i, j, 0] = 0;
                    Grid.Rows[i].Cells[j].Value = null;
                }
            }

            int sup_count_mines = 0;
            int x_cord, y_cord;

            while (sup_count_mines < count_mines)
            {
                x_cord = rand.Next(0, size_field);
                y_cord = rand.Next(0, size_field);
                if (mines[x_cord, y_cord, 0] == 0)
                {
                    mines[x_cord, y_cord, 0] = -1;
                    //Grid.Rows[x_cord].Cells[y_cord].Style.BackColor = Color.DarkRed;
                    sup_count_mines++;
                }
            }
        }


        Random rand = new Random();
        const int size_field = 10;
        const int count_mines = 15;
        int[,,] mines = new int[size_field, size_field, 8];
        int scores = 0, conservation = 0;
        int[] memory_scores = new int[8];

        public void save()
        {
            conservation++;
            for (int i = 0; i < size_field - 1; i++)
            {
                for (int j = 0; j < size_field - 1; j++)
                {
                    mines[i, j, conservation] = mines[i, j, 0];
                }
            }
            memory_scores[conservation-1] = scores;
            listBox1.Items.Insert(conservation - 1, "Игра " + conservation);
        }

        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!timer1.Enabled) return;

            if (mines[e.RowIndex, e.ColumnIndex, 0] == -1)
            {
                int demine = rand.Next(0, 6);
                if (demine < 3)
                {
                    save();
                    MessageBox.Show(
                    "Вы проиграли",
                    "Сообщение",
                    MessageBoxButtons.OK);
                    timer1.Stop();
                }
                else
                {
                    scores += 10;
                    Grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.DarkRed;
                }
            }
            else
            {
                scores += 2;
                int k = 0;
                if (e.RowIndex == 0 && e.ColumnIndex == 0)
                {
                    for (int x = e.RowIndex; x <= e.RowIndex + 1; x++)
                    {
                        for (int y = e.ColumnIndex; y <= e.ColumnIndex + 1; y++)
                        {
                            if (mines[x, y, 0] == -1) k++;
                        }
                    }
                }

                if (e.RowIndex == 0 && e.ColumnIndex == size_field - 1)
                {
                    for (int x = e.RowIndex; x <= e.RowIndex + 1; x++)
                    {
                        for (int y = e.ColumnIndex - 1; y <= e.ColumnIndex; y++)
                        {
                            if (mines[x, y, 0] == -1) k++;
                        }
                    }
                }

                if (e.RowIndex == size_field - 1 && e.ColumnIndex == 0)
                {
                    for (int x = e.RowIndex - 1; x <= e.RowIndex; x++)
                    {
                        for (int y = e.ColumnIndex; y <= e.ColumnIndex + 1; y++)
                        {
                            if (mines[x, y, 0] == -1) k++;
                        }
                    }
                }
                if (e.RowIndex == size_field - 1 && e.ColumnIndex == size_field - 1)
                {
                    for (int x = e.RowIndex - 1; x <= e.RowIndex; x++)
                    {
                        for (int y = e.ColumnIndex - 1; y <= e.ColumnIndex; y++)
                        {
                            if (mines[x, y, 0] == -1) k++;
                        }
                    }
                }
                if (e.RowIndex == 0 && e.ColumnIndex < size_field - 1 && e.ColumnIndex > 0)
                {
                    for (int x = e.RowIndex; x <= e.RowIndex + 1; x++)
                    {
                        for (int y = e.ColumnIndex - 1; y <= e.ColumnIndex + 1; y++)
                        {
                            if (mines[x, y, 0] == -1) k++;
                        }
                    }
                }
                if (e.RowIndex == size_field - 1 && e.ColumnIndex < size_field - 1 && e.ColumnIndex > 0)
                {
                    for (int x = e.RowIndex - 1; x <= e.RowIndex; x++)
                    {
                        for (int y = e.ColumnIndex - 1; y <= e.ColumnIndex + 1; y++)
                        {
                            if (mines[x, y, 0] == -1) k++;
                        }
                    }
                }

                if (e.RowIndex > 0 && e.RowIndex < size_field - 1 && e.ColumnIndex == 0)
                {
                    for (int x = e.RowIndex - 1; x <= e.RowIndex + 1; x++)
                    {
                        for (int y = e.ColumnIndex; y <= e.ColumnIndex + 1; y++)
                        {
                            if (mines[x, y, 0] == -1) k++;
                        }
                    }
                }

                if (e.RowIndex > 0 && e.RowIndex < size_field - 1 && e.ColumnIndex == size_field - 1)
                {
                    for (int x = e.RowIndex - 1; x <= e.RowIndex + 1; x++)
                    {
                        for (int y = e.ColumnIndex - 1; y <= e.ColumnIndex; y++)
                        {
                            if (mines[x, y, 0] == -1) k++;
                        }
                    }
                }

                if (e.RowIndex > 0 && e.RowIndex < size_field - 1 && e.ColumnIndex < size_field - 1 && e.ColumnIndex > 0)
                {
                    for (int x = e.RowIndex - 1; x <= e.RowIndex + 1; x++)
                    { 
                        for (int y = e.ColumnIndex - 1; y <= e.ColumnIndex + 1; y++)
                        {
                            if (mines[x, y, 0] == -1) k++;
                        }
                    }
                }

                Grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = k;
            }
            label2.Text = scores.ToString();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = memory_scores[listBox1.SelectedIndex].ToString();
            for (int i = 0; i < size_field; i++)
            {
                for (int j = 0; j < size_field; j++)
                {
                    Grid.Rows[i].Cells[j].Value = mines[i, j, listBox1.SelectedIndex];
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Init();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            scores -= 1;
            label2.Text = scores.ToString();
        }
    }
}
