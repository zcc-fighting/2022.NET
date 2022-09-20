using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem
{
    //原本计划用户能够查询交易记录的，因为时间关系放弃了
    //以后有机会再加上
    //用户交易记录结构体
    //struct UserRecords
    //{
    //    public string UserName; 
    //    public string Type;
    //    public float Amount;
    //    public float UserBalance;
    //}
    public class Account
    {
        public string UserName { get; set; }//用户名
        public string Password { get; set; }//密码
        //用户余额
        public float UserBalance { get; set; }
        ////交易记录
        //public List<UserRecords> Records = new List<UserRecords>();
        //无参构造函数
        public Account() { }
        //含两个参数的构造函数
        public  Account(string name,string psd) { 
            this.UserName = name;
            this.Password = psd;
        }
        //查询余额
        public float GetBalance()
        {
            return UserBalance;
        }
        
        //存钱
        public void SaveMoney(float saveamount) { 
            //假设有百分之四十几率受到假钞
            Random r = new Random();
            int badmoney = r.Next(0, 5);
            //badmoney取值可能为 0 1 2 3 4
            if (badmoney >= 3)
            {
                throw new BadMoneyException("收到假币，已销毁！\n"+"其余金额已退还，请重新办理！", 001);
            }
            else {
                UserBalance += saveamount;
            }

        }
        //取钱
        public void GetMoney(float getamount) { 
            UserBalance -= getamount;
        }
    }

    public class CreditAccount : Account {
        //用户信用等级
        public int UserCredit { get; set; }
        //用户未还款金额
        public float UserLoanAmount { get; set; } 
        //查询信用等级
        public int GetCredit()
        {
            return UserCredit;
        }
        
        //贷款
        //根据信用等级决定最大贷款金额
        public void loan(float loanamount) {
            UserLoanAmount += loanamount;
            switch (UserCredit) { 
                case 0:
                    if (UserLoanAmount > 100) {
                        UserLoanAmount -= loanamount;
                        throw new ExceededCreditLimitException("超出信用额度！", 101);
                    }
                    else UserBalance += loanamount;
                    break;
                case 1:
                    if (UserLoanAmount > 1000)
                    {
                        UserLoanAmount -= loanamount;
                        throw new ExceededCreditLimitException("超出信用额度！", 102);
                    }
                    else UserBalance += loanamount;
                    break;
                case 2:
                    if (UserLoanAmount > 10000)
                    {
                        UserLoanAmount -= loanamount;
                        throw new ExceededCreditLimitException("超出信用额度！", 103);
                    }
                    else UserBalance += loanamount;
                    break;
            }
        }
    }

}
