using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BankSystem
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //创建账户信息
            //判断用户名和密码不为空且两次输入的密码一致才可以创建账户
            if (textBox1.Text != " " && textBox2.Text != " " && textBox2.Text == textBox3.Text)
            {
                Bank.register(textBox1.Text, textBox2.Text);//调用银行注册函数
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                MessageBox.Show("注册成功！");
            }
            else if (textBox1.Text == " " || textBox2.Text == " " || textBox3.Text == " ")
            {
                MessageBox.Show("用户名和密码不能为空！");
            }
            else if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("两次输入的密码不一致！");
            }
            //返回登录窗口
            this.Close();
        }
    }
}
