using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamSimulation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int time1 = 60;
        public int time2 = 15;
        public int time3 = 2;
        public bool isEnd = false;
        public int a, b;
        public char C;
        public int score = 0;
        //总时长倒计时
        private void timer1_Tick(object sender, EventArgs e)
        {
            time1 = time1 - 1;
            label6.Text = time1 + "'";
            //结束处理
            if (time1 == 0)
            {
                //停止计时
                timer1.Stop();
                timer2.Stop();
                //恢复初始状态
                isEnd = true;
                button1.Text = "开始答题";
                label1.Text = "a";
                label3.Text = "b";
                label6.Text = "60'";
                label8.Text = "15'";
                label10.Text = "0";
                //给出结果
                MessageBox.Show("考试结束!" +
                    "你的得分为：" + score + "!");

            }
        }
        //单题倒计时
        private void timer2_Tick(object sender, EventArgs e)
        {
            time2 = time2 - 1;
            label8.Text = time2 + "'";
            //超时处理
            if (time2 == 0)
            {
                //重新计时
                time2 = 15;
                timer2.Enabled = true;
                timer2.Start();
                time3 = 2;
                timer3.Enabled = true;
                timer3.Start();

                //反馈
                label11.Text = "超时！";
                label11.ForeColor = Color.Red;
                //出题
                Random ra = new Random();
                a = ra.Next(1, 10);
                label1.Text = a.ToString();
                char[] c = new char[2] { '+', '-' };
                C = c[ra.Next(0, 2)];
                label2.Text = C.ToString();
                b = ra.Next(1, 10);
                label3.Text = b.ToString();
            }
        }
        //开始答题按钮
        private void button1_Click(object sender, EventArgs e)
        {
            //开始计时
            time1 = 60;
            timer1.Enabled = true;
            timer1.Start();
            time2 = 15;
            timer2.Enabled = true;
            timer2.Start();
            isEnd = false;
            score = 0;
            label10.Text = score.ToString();
            button1.Text = "重新答题";

            //出题
            Random ra = new Random();
            a = ra.Next(1, 10);
            label1.Text = a.ToString();
            char[] c = new char[2] { '+', '-'};
            C = c[ra.Next(0, 2)];
            label2.Text = C.ToString();
            b = ra.Next(1, 10);
            label3.Text = b.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //回答结果消息计时

        private void timer3_Tick(object sender, EventArgs e)
        {
            time3 = time3 - 1;
            if (time3 == 0) {
                timer3.Stop();
                label11.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int yourResult;
            int.TryParse(textBox1.Text, out yourResult);
            if (isEnd == false)
                switch (C)
                {
                    case '+':
                        if (yourResult == a + b)
                        {
                            score += 5;
                            label10.Text = score.ToString();
                            label11.Text = "回答正确！";
                            label11.ForeColor = Color.Green;
                        }
                        else
                        {
                            label11.Text = "回答错误！";
                            label11.ForeColor = Color.Red;  
                        }
                        break;
                    case '-':
                        if (yourResult == a - b)
                        {
                            score += 5;
                            label10.Text = score.ToString();
                            label11.Text = "回答正确！";
                            label11.ForeColor = Color.Green;

                        }
                        else
                        {
                            label11.Text = "回答错误！";
                            label11.ForeColor = Color.Red;
                        }
                        break;
                }
            //重新计时
            time2 = 15;
            timer2.Enabled = true;
            timer2.Start();
            time3 = 2;
            timer3.Enabled = true;
            timer3.Start();
            //出题
            Random ra = new Random();
            a = ra.Next(1, 10);
            label1.Text = a.ToString();
            char[] c = new char[2] { '+', '-' };
            C = c[ra.Next(0, 2)];
            label2.Text = C.ToString();
            b = ra.Next(1, 10);
            label3.Text = b.ToString();
            //清空答案框
            textBox1.Text = "";
        }
    }
}
