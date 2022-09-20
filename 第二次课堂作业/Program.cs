using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankSystem
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ATMForm.BigMoneyFetched += ATMForm.alertBigMoney;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    //声明参数类型，事件对象
    public class BigMoneyArgs : EventArgs
    {
        public Account account;//账户信息
        public float fetchedmoney;//输入取款金额
    }
    //声明委托类型
    public delegate void BigMoneyFetchedEventHandler(object sender, BigMoneyArgs e);


    //自定义异常类型,表示用户存钱时检测到假钞
    public class BadMoneyException : ApplicationException
    {
        private int idNumber;
        public BadMoneyException(String message, int id) : base(message)
        { this.idNumber = id; }

    }
    //自定义异常类型，表示贷款时超出信用额度
    public class ExceededCreditLimitException : ApplicationException 
    {
        private int idNumber;
        public ExceededCreditLimitException(String message, int id) : base(message)
        { this.idNumber = id; }
    }
}
