using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem
{
    internal class ATM
    {
        //登录函数，同Bank
        public static Account login(string name, string psd)
        {
            foreach (Account i in Bank.getUserAccounts())
            {
                if (i.UserName == name && i.Password == psd)
                {
                    return i;
                }
            }
            return null;

        }
    }
}
