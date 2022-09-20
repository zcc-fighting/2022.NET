using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BankSystem
{
    public partial class ATMForm : Form
    {
        //区分不同业务
        public char type;
        public ATMForm()
        {
            InitializeComponent();
        }
        //界面初始化
        private void ATMForm_Load(object sender, EventArgs e)
        {
            label1.Text = Form1.currentAccount.UserName;
            //判断登录状态
            if (Form1.isloginSuccess_ATM && !Form1.isloginSuccess_Bank)
                button8.Text = "切换银行登录";
            else if (!Form1.isloginSuccess_ATM && Form1.isloginSuccess_Bank)
                button8.Text = "切换ATM登录";
            else { MessageBox.Show("登录错误！"); }
        }
        //注册事件，ATM大额取钱处理
        public static event BigMoneyFetchedEventHandler BigMoneyFetched;
        //大额取款处理
        public static void alertBigMoney(object sender, BigMoneyArgs e)
        {
            //弹出窗口警告
            MessageBox.Show("超出限额！请前往银行办理！");
        }

        //查询余额
        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = "您的账户余额为："+Form1.currentAccount.GetBalance();
            textBox1.Visible = false;
            button4.Visible = false;
        }
        //查询信誉等级
        private void button5_Click(object sender, EventArgs e)
        {
            label2.Text = "您的信用等级为："+(Form1.currentAccount.GetCredit()+1);
            textBox1.Visible = false;
            button4.Visible = false;
        }
        //存钱
        private void button2_Click(object sender, EventArgs e)
        {
            label2.Text = "请输入存款金额：";
            textBox1.Visible = true;
            button4.Visible = true;
            type = 'S';//Save
        }
        //取钱
        private void button3_Click(object sender, EventArgs e)
        {
            label2.Text = "请输入取款金额：";
            textBox1.Visible = true;
            button4.Visible = true;
            type = 'G';//Get
        }
        //贷款
        private void button6_Click(object sender, EventArgs e)
        {
            label2.Text = "请输入贷款金额：";
            textBox1.Visible = true;
            button4.Visible = true;
            type = 'L';//Loan
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label2.Text = "请在左侧选择您要办理的业务";
            textBox1.Visible=false;
            button4.Visible=false;
            type = ' ';
        }

        private void button4_Click(object sender, EventArgs e)
        {
            float amount = Convert.ToInt32(textBox1.Text);
            //要确保输入为数字
            if (textBox1.Text != null)
            {
                switch (type)
                {
                    case 'S':
                        try
                        {
                            Form1.currentAccount.SaveMoney(amount);
                            MessageBox.Show("账户已存入：" + amount + "元！\n" +
                                "当前帐户余额：" + Form1.currentAccount.GetBalance() + "元！");
                            //每次成功存钱均会增加信用等级,最高可达到三级
                            //信誉等级越高，可贷款数目越多
                            if(Form1.currentAccount.UserCredit<3)
                                Form1.currentAccount.UserCredit++;
                            
                        }
                        catch(BadMoneyException ex) {
                            textBox1.Clear();
                            //收到假币后会把该账户信誉值清零
                            Form1.currentAccount.UserCredit = 0;
                            MessageBox.Show(ex.Message);
                        }
                        break;
                    case 'G':
                        //ATM大额取款
                        if (amount > 10000 && Form1.isloginSuccess_ATM)
                        {
                            BigMoneyArgs args = new BigMoneyArgs();
                            args.account = Form1.currentAccount;
                            args.fetchedmoney = float.Parse(textBox1.Text);
                            BigMoneyFetched(this, args);
                        }
                        else if (Form1.currentAccount.GetBalance() - amount >= 0)
                        {
                            Form1.currentAccount.GetMoney(amount);
                            MessageBox.Show("账户已取出：" + amount + "元！\n" +
                                "当前帐户余额：" + Form1.currentAccount.GetBalance() + "元！");
                        }
                        else
                        {
                            MessageBox.Show("账户余额不足！\n" + "请选择其他业务！");
                        }
                        break;
                    case 'L':
                        try {
                            Form1.currentAccount.loan(amount);
                            MessageBox.Show("账户已贷款：" + amount + "元！\n" +
                                "当前帐户余额：" + Form1.currentAccount.GetBalance() + "元！");
                        }
                        catch (ExceededCreditLimitException ex){
                            textBox1.Clear();
                            MessageBox.Show(ex.Message);
                        }
                        
                        break;
                    default:
                        break;
                }
                textBox1.Text = "";
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Form1.isloginSuccess_ATM)
            {
                button8.Text = "切换ATM登录";
                Form1.isloginSuccess_ATM = false;
                Form1.isloginSuccess_Bank = true;
            }
            else if (Form1.isloginSuccess_Bank)
            {
                button8.Text = "切换ATM登录";
                Form1.isloginSuccess_Bank = false;
                Form1.isloginSuccess_ATM = true;
            }
            else {
                MessageBox.Show("切换失败！");
            }
        }

        private void ATMForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //退出确认
            //if (DialogResult.OK == MessageBox.Show("你确定要退出吗？", "关闭提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            //{
            //    this.FormClosing -= new FormClosingEventHandler(this.ATMForm_FormClosing); //这里是  -=
            //    Application.Exit();  //退出进程
            //}

            //else
            //{
            //    e.Cancel = true;  //取消。返回窗体
            //}
        }
    }
}
