using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem
{

    internal class Bank
    {
        //账户信息
        static List<Account> UserAccounts=new List<Account>();
        //信用账户信息
        static List<CreditAccount> UserCreditAccounts =new List<CreditAccount>();

        //用户注册函数
        public static void  register(string name,string psd) {
            CreditAccount newAccount = new CreditAccount();
            newAccount.UserName= name;
            newAccount.Password= psd;
            UserAccounts.Add(newAccount);
        }
        //登录函数，检验输入的账号是否存在以及密码是否正确
        public static CreditAccount login(string name, string psd)
        {
            foreach (CreditAccount i in UserAccounts)
            {
                if (i.UserName == name && i.Password == psd)
                {
                    return i;
                }
            }
            return null;
        }
        //获取用户注册表
        public static List<Account> getUserAccounts() { return UserAccounts; }
        public static List<CreditAccount> getUserCreditAccounts() { return UserCreditAccounts; }


    }
}
