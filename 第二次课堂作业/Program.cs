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

    //�����������ͣ��¼�����
    public class BigMoneyArgs : EventArgs
    {
        public Account account;//�˻���Ϣ
        public float fetchedmoney;//����ȡ����
    }
    //����ί������
    public delegate void BigMoneyFetchedEventHandler(object sender, BigMoneyArgs e);


    //�Զ����쳣����,��ʾ�û���Ǯʱ��⵽�ٳ�
    public class BadMoneyException : ApplicationException
    {
        private int idNumber;
        public BadMoneyException(String message, int id) : base(message)
        { this.idNumber = id; }

    }
    //�Զ����쳣���ͣ���ʾ����ʱ�������ö��
    public class ExceededCreditLimitException : ApplicationException 
    {
        private int idNumber;
        public ExceededCreditLimitException(String message, int id) : base(message)
        { this.idNumber = id; }
    }
}
