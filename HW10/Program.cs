using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS422
{
    class Program
    {
        static void Main(string[] args)
        {
            BigNum teddy = new BigNum(12345678912345678.123123123123, false);
            double dog = 12345678912345678.123123123123;
            BigNum left = new BigNum(0116.8410,false);
            BigNum right = new BigNum("123.123123123");
            BigNum negRight = new BigNum("-1.8410");


            BigNum result = left - right;
                Console.WriteLine(result.ToString());
            //string temp = result.ToString();
        }
    }
}
