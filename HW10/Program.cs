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
            BigNum left = new BigNum("100.5");
            BigNum right = new BigNum("100.4999");
            //BigNum result = left / right;
            if (right <= left)
                Console.WriteLine("left Greater");
            //string temp = result.ToString();
        }
    }
}
