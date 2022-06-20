using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PXZ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Label[,] arrLbl = new Label[3, 3];
        int unRow = 0, unCol = 0;
        bool playing = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Play")
            {
                button1.Text = "Pause";
                this.Text = "Game Playing...";
                rmblock();
            }
            else if (button1.Text == "Pause")
            {
                button1.Text = "Continue";
                this.Text = "Game Pauseing...";
                gpause();
            }
            else if (button1.Text == "Continue") 
            {
                this.Text = "Game Playing...";
                button1.Text = "Pause";
                gcontinue();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (!playing) 
            {
                return;
            }
            int row = ((Label)sender).Top / 100;
            int col = ((Label)sender).Left / 100;
            if (Math.Abs(row - unRow) + Math.Abs(col - unCol) == 1)
            {
                string temp = arrLbl[unRow, unCol].Text;        //存根字符
                Color temps = arrLbl[unRow, unCol].BackColor;   //存根颜色

                arrLbl[unRow, unCol].BackColor = arrLbl[row, col].BackColor;
                arrLbl[unRow, unCol].Text = arrLbl[row, col].Text;

                arrLbl[row, col].Text = temp;
                arrLbl[row, col].BackColor = temps;

                arrLbl[unRow, unCol].Visible = true;
                arrLbl[row, col].Visible = false;
                unRow = row;
                unCol = col;
            }
            for (int i = 0; i < 9; i++)
            {
                if (arrLbl[i / 3, i % 3].Text != Convert.ToString(i + 1))
                {
                    break;
                }
                if (i == 8)
                {
                    arrLbl[unRow, unCol].Visible = true;
                    playing = false;
                    MessageBox.Show("Game Over!");
                    button1.Text = "Play";
                }
            }
        }

        public void rmblock() 
        {
            arrLbl1();
            arrLbl[unRow, unCol].Visible = true;
            int[] arrNum = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Random rm = new Random();
            for (int i = 0; i < 8; i++)
            {
                int rmNum = rm.Next(i, 9);
                int temp = arrNum[i];
                arrNum[i] = arrNum[rmNum];
                arrNum[rmNum] = temp;
            }
            for (int i = 0; i < 9; i++)
            {
                arrLbl[i / 3, i % 3].Text = arrNum[i].ToString();
                //     1/3=0  1%3=0
                //     2/3=0  2%3=1....
            }
            int cover = rm.Next(0, 9);
            unRow = cover / 3;
            unCol = cover % 3;
            arrLbl[unRow, unCol].Visible = false; //随机一个label隐藏
            playing = true;
        }

        public void arrLbl1() 
        {
            arrLbl[0, 0] = label1;
            arrLbl[0, 1] = label2;
            arrLbl[0, 2] = label3;
            arrLbl[1, 0] = label4;
            arrLbl[1, 1] = label5;
            arrLbl[1, 2] = label6;
            arrLbl[2, 0] = label7;
            arrLbl[2, 1] = label8;
            arrLbl[2, 2] = label9;
        }

        public void gpause() 
        {
            arrLbl1();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    arrLbl[i, j].Enabled = false;
                }
            }
        }

        public void gcontinue()
        {
            arrLbl1();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    arrLbl[i, j].Enabled = true;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5)
            {
                button1.Text = "Pause";
                this.Text = "Game Playing...";
                rmblock();
                gcontinue();
            }
            else if (e.KeyData == Keys.Escape)
            {
                System.Environment.Exit(0);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "";
        }

    }
}
