using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_1
{
    public partial class Form1 : Form
    {
        static int left;
        static int top;
        static int attempt = 0;

        public Form1()
        {
            InitializeComponent();
            Width = 400;
            Height = 300;
            StartPosition = FormStartPosition.Manual;
            left = (Screen.PrimaryScreen.Bounds.Width - 300) / 2;
            top = (Screen.PrimaryScreen.Bounds.Height - 300) / 2;
            Location = new Point(left, top);
            KeyPreview = true; 
        }

        private int [,]less(int len, int[,] nums)
        {

            int min = nums[len, 0];
            int max = nums[len, 1];

            nums[len + 1, 0] = min;
            nums[len + 1, 1] = min + (max - min) / 2;

            return nums;
        }

        private int [,]more(int len,int[,] nums)
        {
            int min = nums[len, 0];
            int max = nums[len, 1];

            nums[len + 1, 0] = min + (max - min) / 2;
            nums[len + 1, 1] = max;
 
            return nums;
        }


        int Play(int[,] nums)
        {
            attempt++;
            
            //int len = attempt;
            int min = nums[attempt - 1, 0];
            int max = nums[attempt - 1, 1];            

            DialogResult result = MessageBox.Show("Ваше число : " + less(attempt - 1 , nums)[attempt, 1] + " ?", "Окна сообщений", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            while (result != DialogResult.Yes)
            {
                if (result == DialogResult.Cancel)
                {
                    Application.Exit();
                }
                    else if (result == DialogResult.No)
                    {
                        result = MessageBox.Show("Ваше число больше " + less(attempt - 1,nums)[attempt, 1] + " ?", "Окна сообщений", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                        if (result == DialogResult.Cancel)
                        {
                            Application.Exit();
                        }
                        else if (result == DialogResult.Yes)
                        { 
                            Play(more(attempt - 1,nums));
                        }
                        else if (result == DialogResult.No)
                        {
                            Play(less(attempt - 1, nums));
                        }
                    }
                return attempt;
            }
            return attempt;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Form frm = (Form)sender;

            int min = 1, max = 100;

            int arrayElements = (int)Math.Log(max,2) + 2;

            int[,] nums = new int[arrayElements, 2];
            nums[0, 0] = min;
            nums[0, 1] = max;

            if (e.KeyCode == Keys.Return)
            {            
                DialogResult result = MessageBox.Show("Загадайте число от " +  min + " до " + max, "Окна сообщений",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    frm.Text = "Число угадано с " + Play(nums).ToString() + " попытки.";
                }
                else if (result == DialogResult.Cancel)
                {
                    MessageBox.Show("До свидания!", "Окна сообщений");
                }                               

                result = MessageBox.Show("Сыграем еще?", "Окна сообщений", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    attempt = 0;
                    Form1_KeyDown(sender,e);
                    //frm.Text = Play(nums).ToString();
                }
                else if (result == DialogResult.Cancel)
                {
                    MessageBox.Show("До свидания!", "Окна сообщений");
                        Application.Exit();
                }
            }                  
                
            else if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Text = DateTime.Now.ToString();
        }
    }
}
