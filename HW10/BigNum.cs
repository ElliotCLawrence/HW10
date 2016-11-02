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

            bool encounteredDecimal = false;

            for (int x = 0; x < number.Length; x++)
            {
                if (number[x] == '-')
                {
                    if (x > 0) //if x is greater than 0, no '-' allowed.
                        throw new Exception();
                }
                    
                else if (number[x] == '.')
                {
                    if (encounteredDecimal == false) //first decimal is ok.
                        encounteredDecimal = true;
                    else                             //second decimal is not ok.
                        throw new Exception();
                }
                 

                else if (number[x] < '0' || number[x] > '9') //if it's not a '-' or '.' it must be 0-9 , if not, throw exception.
                    throw new Exception();

            }
            //valid string.

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
