using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS422
{
    class BigNum
    {
        private string bigNum;

        public BigNum(string number)
        {
            
            char[] validStart = { '-', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };
            if (number == null || number == "")
                throw new Exception();
            if (!validStart.Contains(number[0])) //check to make sure the first char is a valid starting character
                throw new Exception();
            if (number.Contains(" ")) //check for white space
                throw new Exception();

            /*
             * The only allowed characters are
               ‘-‘ (at most 1 and only ever valid as the very first character)
               ‘.’ (at most 1)
               [0-9]
            */

            bigNum = number;
        }

        public BigNum(double value, bool useDoubleToString)
        {

        }

        public override string ToString()
        {
            return base.ToString();
        }

        public bool IsUndefined
        {
            get
            {
                return true; //change this
            }
        }

        public static BigNum operator + (BigNum lhs, BigNum rhs)
        {
            return new BigNum("dog");
        }
        public static BigNum operator - (BigNum lhs, BigNum rhs)
        {
            return new BigNum("dog");
        }
        public static BigNum operator * (BigNum lhs, BigNum rhs)
        {
            return new BigNum("dog");
        }
        public static BigNum operator /(BigNum lhs, BigNum rhs)
        {
            return new BigNum("dog");
        }
        public static BigNum operator >(BigNum lhs, BigNum rhs)
        {
            return new BigNum("dog");
        }
        public static BigNum operator >=(BigNum lhs, BigNum rhs)
        {
            return new BigNum("dog");
        }
        public static BigNum operator <(BigNum lhs, BigNum rhs)
        {
            return new BigNum("dog");
        }
        public static BigNum operator <=(BigNum lhs, BigNum rhs)
        {
            return new BigNum("dog");
        }

        public static bool IsToStringCorrect(double value)
        {
            return true; //change this
        }

    }
}
