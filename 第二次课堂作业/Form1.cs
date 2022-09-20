using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankSystem
{
    public partial class Form1 : Form
    {
        //登录状态
        public static bool isloginSuccess_Bank = false;
        public static bool isloginSuccess_ATM = false;
        //用于记录最近登录的账户
        public static CreditAccount currentAccount;
        public Form1()
        {
            InitializeComponent();
        }
       

        private void button2_Click(object sender, EventArgs e)
        {
            //打开注册窗口
            Form2 form2 = new Form2();
            form2.Show();
        }
        //ATM登录
        private void button1_Click(object sender, EventArgs e)
        {
            //判断账户密码是否正确
            foreach (CreditAccount i in Bank.getUserAccounts())
            {
                if (textBox1.Text == i.UserName && textBox2.Text == i.Password)
                {
                    currentAccount = i;//目前执行操作的用户
                    textBox2.Clear();
                    isloginSuccess_ATM = false;
                    isloginSuccess_ATM = true;
                    MessageBox.Show("登录成功");
                    break;
                }
            }
            if (!isloginSuccess_ATM)
            {
                textBox1.Clear();
                textBox2.Clear();
                MessageBox.Show("登录错误");
            }
            else {
                //打开业务界面
                ATMForm aTMForm = new ATMForm();
                aTMForm.Show();
                //关闭登录界面
                this.Hide();
            }
        }

        //Bank登录
        private void button3_Click(object sender, EventArgs e)
        {
            //判断账户密码是否正确
            foreach (CreditAccount i in Bank.getUserAccounts())
            {
                if (textBox1.Text == i.UserName && textBox2.Text == i.Password)
                {
                    currentAccount = i;//目前执行操作的用户
                    textBox2.Clear();
                    isloginSuccess_ATM = false;
                    isloginSuccess_Bank = true;
                    MessageBox.Show("登录成功");
                    break;
                }
            }
            if (!isloginSuccess_Bank)
            {
                textBox1.Clear();
                textBox2.Clear();
                MessageBox.Show("登录错误");
            }
            else
            {
                //打开业务界面
                ATMForm aTMForm = new ATMForm();
                aTMForm.Show();
                //关闭登录界面
                this.Hide();
            }
        }
    }
}
