//Elliot Lawrence
//ID11349302
//HW10
//CS422

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Collections;

namespace CS422
{
    class BigNum
    {
        
        private BigInteger baseInt;
        private int exponent;
        private bool isNegative;
        private bool isUndefined;

        public BigNum(string number)
        {
            initializeWithString(number);
        }

        private BigNum(BigInteger baseInteger, int exponentValue) //private constructor , helps in other functions
        {
            baseInt = BigInteger.Abs(baseInteger);
            exponent = exponentValue;

            if (baseInteger > 0)
                isNegative = false;
            else
                isNegative = true;

            isUndefined = false;
        }

        public BigNum(double value, bool useDoubleToString) 
        {
            if (Double.IsNaN(value) || Double.IsInfinity(value) || Double.IsNegativeInfinity(value))
            {
                baseInt = new BigInteger();
                exponent = 0;
                isUndefined = true;
                return;
            }

            
            if (useDoubleToString)
            {
                string number = value.ToString();
                
            } //double.tostring() end

            else //Cannot use double.tostring()
            {
                byte[] byteArray = BitConverter.GetBytes(value);
                var bits = new BitArray(byteArray);
                
                for (int x = 0; x < 32; x ++) //bit array needs to be reversed on windows.
                {
                    bool curBit = bits[x];
                    bits[x] = bits[bits.Length - x - 1];
                    bits[bits.Length - x - 1] = curBit;
                }


                bool isPositive = true;
                int localexponent = 0;
                

                if (bits[0] == true)
                    isPositive = false;

                string exponentStr = "";
                for (int x = 1; x < 12; x++)//populate bits into a string
                {
                    if (bits[x] == true)
                        exponentStr += "1";
                    else
                        exponentStr += "0";
                }

                localexponent = Convert.ToInt32(exponentStr, 2) - 1023; //exponent (math from wikipedia)

                string fractionStr = "";

                for (int x = 12; x < 64; x++) //populate bits into a string
                {
                    if (bits[x] == true)
                        fractionStr += "1";
                    else
                        fractionStr += "0";
                }

                decimal dec = 1;
                for (int x = 1; x <= 52; x++)
                {
                    if (fractionStr[x-1] == '1') // if bit is true
                    { 
                        dec += (decimal)Math.Pow(2, -x);
                    }
                }

                string actualNum = "";

                if (isPositive == false)
                {
                    actualNum += "-";
                }

                 actualNum += (dec * (decimal)Math.Pow(2, localexponent)).ToString();

                initializeWithString(actualNum);
            }
        }

        private void initializeWithString(string number)
        {
            isUndefined = false; //if we're here, you can assume the BigNum is defined.

            int x = 0;
            char[] validStart = { '-', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };
            if (number == null || number == "")
                throw new Exception();
            if (!validStart.Contains(number[0])) //check to make sure the first char is a valid starting character
                throw new Exception();
            if (number.Contains(" ")) //check for white space
                throw new Exception();

            bool encounteredDecimal = false;
            int decimalLocation = -1;

            
            for (x = 0; x < number.Length; x++)
            {
                if (number[x] == '-')
                {
                    if (x > 0) //if x is greater than 0, no '-' allowed.
                        throw new Exception();
                }

                else if (number[x] == '.')
                {
                    if (encounteredDecimal == false) //first decimal is ok.
                    {
                        encounteredDecimal = true;
                        decimalLocation = x;
                    }

                    else                             //second decimal is not ok.
                        throw new Exception();
                }

                else if (number[x] < '0' || number[x] > '9') //if it's not a '-' or '.' it must be 0-9 , if not, throw exception.
                    throw new Exception();
            }
            //valid string.

            x = 0;

            string numberNoDecimal = "";
            isNegative = false;

            while (x < number.Length)
            {
                if (number[x] == '.')
                {
                    x++;
                    continue;
                }
                   
                else if (number[x] == '-')
                {
                    x++;
                    isNegative = true;
                    continue;
                }

                numberNoDecimal += number[x];
                x++;
            }

            baseInt = new BigInteger(); //get a new integer
            baseInt = 0;

            BigInteger multiplyCoEfficient = new BigInteger();
            multiplyCoEfficient = 1; 

            for (x = numberNoDecimal.Length -1; x >= 0; x--)
            {
                baseInt += (numberNoDecimal[x] - '0') * multiplyCoEfficient;
                multiplyCoEfficient *= 10;
            }

            baseInt = BigInteger.Abs(baseInt);


            if (!encounteredDecimal) //if no decimal
            {
                exponent = 0;
            }

            else //there was a decimal
            {
                if (isNegative)
                    decimalLocation--;
                exponent = ((numberNoDecimal.Length - decimalLocation) * -1);
            }


        }

        public override string ToString() //assumes that the base number + 1 is less than integer maximum characters.
        {
            try
            {
                if (this.isUndefined)
                    return "undefined";

                string number = baseInt.ToString();
                bool onlyZero = true;

                for (int x = 0; x < number.Length; x++)
                {
                    if (number[x] != '0' && number[x] != '.')
                    {
                        onlyZero = false;
                        break;
                    }
                }

                if (onlyZero)
                    return "0";
                
                if ((exponent*-1) > number.Length) //need pre-pending 0s if the number is just a decimal
                {
                    while ((exponent*-1) > number.Length)
                    {
                        number = number.Insert(0, "0");
                    }
                }

                if (exponent != 0)
                {
                    number = number.Insert((number.Length) + exponent, "."); //exponent will always be <= 0
                }
                

                if (number[0] == '0') //if it starts with a 0, add a 0. to the beginning
                    number = number.Insert(0, "0.");
                else if (number[0] == '.') //if it starts with a '.' add a 0 preceding the number.
                    number = number.Insert(0, "0");

                if (isNegative) //if it's negative add a negative sign to it
                    number = number.Insert(0, "-");


                if (number.Contains(".")) //trim trailing 0s
                {
                    int x = number.Length;

                    while (number[x - 1] == '0')
                    {
                        x--;
                        number = number.Remove(x);
                    }                  
                }

                return number;
            }

            catch //if bigInt is larger than int max characters after tostring() it will fail.
            {
                throw new Exception("Base int too large to convert to string");
            }
        }

        public bool IsUndefined
        {
            get
            {
                return isUndefined; //change this
            }
        }

        public static BigNum operator + (BigNum lhs, BigNum rhs)
        {
            if (rhs.isUndefined || lhs.isUndefined) //cannot divide by 0
                return new BigNum(Double.NaN, false);

            BigInteger lhsBase = lhs.baseInt;
            BigInteger rhsBase = rhs.baseInt;
            int lhsExp = lhs.exponent;
            int rhsExp = rhs.exponent;


            if (lhs.isNegative)                 //get negatives
                lhsBase = lhs.baseInt * -1;
            if (rhs.isNegative)
                rhsBase = rhs.baseInt * -1;

            while (lhsExp > rhsExp) //get them to the same ammount of digits
            {
                lhsBase *= 10;
                lhsExp--;
            }
            while (lhsExp < rhsExp)
            {
                rhsBase *= 10;
                rhsExp--;
            }

            BigInteger newBase = lhsBase + rhsBase;


            return new BigNum(newBase, lhsExp);
        }

        public static BigNum operator - (BigNum lhs, BigNum rhs)
        {
            if (rhs.isUndefined || lhs.isUndefined) //cannot divide by 0
                return new BigNum(Double.NaN, false);

            BigInteger lhsBase = lhs.baseInt;
            BigInteger rhsBase = rhs.baseInt;
            int lhsExp = lhs.exponent;
            int rhsExp = rhs.exponent;


            if (lhs.isNegative)                 //get negatives
                lhsBase = lhs.baseInt * -1;
            if (rhs.isNegative)
                rhsBase = rhs.baseInt * -1;

            while (lhsExp > rhsExp) //get them to the same ammount of digits
            {
                lhsBase *= 10;
                lhsExp--;
            }
            while (lhsExp < rhsExp)
            {
                rhsBase *= 10;
                rhsExp--;
            }

            BigInteger newBase = lhsBase - rhsBase;


            return new BigNum(newBase, lhsExp);
        }

        public static BigNum operator * (BigNum lhs, BigNum rhs)
        {
            if (rhs.isUndefined || lhs.isUndefined) //cannot divide by 0
                return new BigNum(Double.NaN, false);

            BigInteger lhsBase = lhs.baseInt;
            BigInteger rhsBase = rhs.baseInt;
            int lhsExp = lhs.exponent;
            int rhsExp = rhs.exponent;

            if (lhs.isNegative) //get negatives
                lhsBase = lhs.baseInt * -1;
            if (rhs.isNegative)
                rhsBase = rhs.baseInt * -1;

            BigInteger baseInt = lhsBase * rhsBase;
            int exp = lhsExp + rhsExp;

            return new BigNum(baseInt, exp);
        }

        public static BigNum operator /(BigNum lhs, BigNum rhs)
        {
            if (rhs.baseInt == 0 || rhs.isUndefined || lhs.isUndefined) //cannot divide by 0
                return new BigNum(Double.NaN, false);

            BigInteger lhsBase = lhs.baseInt;
            BigInteger rhsBase = rhs.baseInt;
            int lhsExp = lhs.exponent;
            int rhsExp = rhs.exponent;

            if (lhs.isNegative) //get negatives
                lhsBase = lhs.baseInt * -1;
            if (rhs.isNegative)
                rhsBase = rhs.baseInt * -1;

            for (int x = 0; x < 30; x++) //precise up to 30 digits
                lhsBase *= 10;

            BigInteger newNumerator = lhsBase / rhsBase;

            int newExponent = lhsExp - rhsExp;

            newExponent -= 30; //bring out the 30 powers

            return new BigNum(newNumerator, newExponent);
        }

        private static BigInteger calculateMask(BigInteger exp)
        {
            BigInteger mask = 1;

            while (exp < 0) //up the mask to equal 10 ^ exp
            {
                mask *= 10;
                exp++;
            }

            return mask;
        }

        public static bool operator >(BigNum lhs, BigNum rhs)
        {
            if (lhs.isNegative && !rhs.isNegative) //if left hand side is negative, and right isn't, return false
                return false;

            if (!lhs.isNegative && rhs.isNegative) //if right hand side is negative, and left isn't, return true
                return true;

            BigInteger lhsMask = calculateMask(lhs.exponent);
            BigInteger rhsMask = calculateMask(rhs.exponent);

            BigInteger lhsDecimal;
            BigInteger rhsDecimal;

            int leftExp = lhs.exponent;
            int rightExp = rhs.exponent;

            if ((lhs.baseInt / lhsMask) > (rhs.baseInt / rhsMask))
                return true;

            if ((lhs.baseInt / lhsMask) == (rhs.baseInt / rhsMask)) //if the left hand side of the decimal is greater or equal on the left operand, return true
            {
                lhsDecimal = lhs.baseInt % lhsMask;
                rhsDecimal = rhs.baseInt % rhsMask;

                while (leftExp > rightExp) //get them to the same ammount of digits
                {
                    lhsDecimal *= 10;
                    leftExp--;
                }
                while (leftExp < rightExp)
                {
                    rhsDecimal *= 10;
                    rightExp--;
                }

                if (lhsDecimal > rhsDecimal)
                    return true;
            }

            return false; //else return false
        }


        public static bool operator >= (BigNum lhs, BigNum rhs)
        {
            if (lhs.isNegative && !rhs.isNegative) //if left hand side is negative, and right isn't, return false
                return false;

            if (!lhs.isNegative && rhs.isNegative) //if right hand side is negative, and left isn't, return true
                return true;

            BigInteger lhsMask = calculateMask(lhs.exponent);
            BigInteger rhsMask = calculateMask(rhs.exponent);

            BigInteger lhsDecimal;
            BigInteger rhsDecimal;

            int leftExp = lhs.exponent;
            int rightExp = rhs.exponent;

            if ((lhs.baseInt / lhsMask) > (rhs.baseInt / rhsMask))
                return true;

            if ((lhs.baseInt / lhsMask) == (rhs.baseInt / rhsMask)) //if the left hand side of the decimal is greater or equal on the left operand, return true
            {
                lhsDecimal = lhs.baseInt % lhsMask;
                rhsDecimal = rhs.baseInt % rhsMask;

                while (leftExp > rightExp) //get them to the same ammount of digits
                {
                    lhsDecimal *= 10;
                    leftExp--;
                }
                while (leftExp < rightExp)
                {
                    rhsDecimal *= 10;
                    rightExp--;
                }

                if (lhsDecimal >= rhsDecimal)
                    return true;
            }

            return false; //else return false
        }

        public static bool operator <(BigNum lhs, BigNum rhs)
        {
            if (lhs.isNegative && !rhs.isNegative) //if left hand side is negative, and right isn't, return false
                return false;

            if (!lhs.isNegative && rhs.isNegative) //if right hand side is negative, and left isn't, return true
                return true;

            BigInteger lhsMask = calculateMask(lhs.exponent);
            BigInteger rhsMask = calculateMask(rhs.exponent);

            BigInteger lhsDecimal;
            BigInteger rhsDecimal;

            int leftExp = lhs.exponent;
            int rightExp = rhs.exponent;

            if ((lhs.baseInt / lhsMask) < (rhs.baseInt / rhsMask))
                return true;

            if ((lhs.baseInt / lhsMask) == (rhs.baseInt / rhsMask)) //if the left hand side of the decimal is greater or equal on the left operand, return true
            {
                lhsDecimal = lhs.baseInt % lhsMask;
                rhsDecimal = rhs.baseInt % rhsMask;

                while (leftExp > rightExp) //get them to the same ammount of digits
                {
                    lhsDecimal *= 10;
                    leftExp--;
                }
                while (leftExp < rightExp)
                {
                    rhsDecimal *= 10;
                    rightExp--;
                }

                if (lhsDecimal < rhsDecimal)
                    return true;
            }

            return false; //else return false

        }

        public static bool operator <=(BigNum lhs, BigNum rhs)
        {
            if (lhs.isNegative && !rhs.isNegative) //if left hand side is negative, and right isn't, return false
                return false;

            if (!lhs.isNegative && rhs.isNegative) //if right hand side is negative, and left isn't, return true
                return true;

            BigInteger lhsMask = calculateMask(lhs.exponent);
            BigInteger rhsMask = calculateMask(rhs.exponent);

            BigInteger lhsDecimal;
            BigInteger rhsDecimal;

            int leftExp = lhs.exponent;
            int rightExp = rhs.exponent;

            if ((lhs.baseInt / lhsMask) > (rhs.baseInt / rhsMask))
                return true;

            if ((lhs.baseInt / lhsMask) == (rhs.baseInt / rhsMask)) //if the left hand side of the decimal is greater or equal on the left operand, return true
            {
                lhsDecimal = lhs.baseInt % lhsMask;
                rhsDecimal = rhs.baseInt % rhsMask;

                while (leftExp > rightExp) //get them to the same ammount of digits
                {
                    lhsDecimal *= 10;
                    leftExp--;
                }
                while (leftExp < rightExp)
                {
                    rhsDecimal *= 10;
                    rightExp--;
                }

                if (lhsDecimal <= rhsDecimal)
                    return true;
            }

            return false; //else return false
        }

        public static bool IsToStringCorrect(double value)
        {
            if (value.ToString() == ((new BigNum(value, false)).ToString()))
                return true;
            return false;
        }
    }
}
