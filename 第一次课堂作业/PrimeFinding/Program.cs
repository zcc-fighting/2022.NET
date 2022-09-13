using System;

namespace PrimeFinding
{
    internal class Program
    {
        //判断是否是素数
        public static bool isPrime(int t) {
            for (int i = 2; i < t - 1; i++)
            {
                if (t % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static int []findingPrime(int[] a) {
            int[] b = new int[a.Length];
            int j = 0;
            for (int i = 0; i < a.Length; i++) {
                if (isPrime(a[i]) == true)
                {
                    b[j] = a[i]; 
                    j++;
                }
            }

            int[]c = new int[j];
            for (int i = 0; i < j; i++)
            {
                c[i] = b[i];
            }

            return c;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("请输入待查找数组：（数字间用逗号隔开）");
            //测试数组：
            //1,2,3,4,5,6,7,8,9,123,456,789
            string Str = Console.ReadLine();
            string[]str = Str.Split(",", StringSplitOptions.None);
            int[]a = new int [str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                a[i] = int.Parse(str[i]);
            }
            int[] result = findingPrime(a);
            for (int i = 0; i < result.Length; i++) {
                Console.WriteLine(result[i]);
            }
        }
    }
}
