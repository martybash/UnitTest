using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public enum Sign
    {
        minus = -1,
        plus = 1
    }
    public class MyInt
    {
        public readonly List<byte> digits = new List<byte>();

        public Sign Sign { get; private set; } = Sign.plus;

        public MyInt(List<byte> bytes)
        {
            digits = bytes.ToList();
            RemoveNulls();
        }
        public MyInt(Sign sign, List<byte> bytes)
        {
            Sign = sign;
            digits = bytes;
            RemoveNulls();
        }
        private void RemoveNulls()
        {
            for (var i = digits.Count - 1; i > 0; i--)
            {
                if (digits[i] == 0)
                    digits.RemoveAt(i);
                else
                    break;
            }
        }
        public MyInt(string str)
        {
            if (str.StartsWith("-"))
            {
                Sign = Sign.minus;
                str = str.Substring(1);
            }
            foreach (var i in str.Reverse())
                digits.Add(Convert.ToByte(i.ToString()));
            RemoveNulls();
        }
        public MyInt(uint x) => digits.AddRange(GetBytes(x));
        public MyInt(long x)
        {
            if (x < 0)
                Sign = Sign.minus;
            digits.AddRange(GetBytes((uint)Math.Abs(x)));
        }
        public MyInt(int x)
        {
            if (x < 0)
                Sign = Sign.minus;
            digits.AddRange(GetBytes((uint)Math.Abs(x)));
        }
        private List<byte> GetBytes(uint num)
        {
            var bytes = new List<byte>();
            while (num > 0)
            {
                bytes.Add((byte)(num % 10));
                num /= 10;
            }
            return bytes;
        }
        public override string ToString()
        {
            var s = new StringBuilder(Sign == Sign.plus ? "" : "-");
            for (var i = digits.Count - 1; i >= 0; i--)
                s.Append(Convert.ToString(digits[i]));
            return s.ToString();
        }
        public int Size => digits.Count;
        public byte GetByte(int i) => i < Size ? digits[i] : (byte)0;
        public void SetByte(int i, byte b)
        {
            while (digits.Count <= i)
            {
                digits.Add(0);
            }
            digits[i] = b;
        }
        public static int compareTo(MyInt a, MyInt b, bool ignoreSign = false)
        {
            return CompareSign(a, b, ignoreSign);
        }
        public static int CompareSign(MyInt a, MyInt b, bool ignoreSign = false)
        {
            if (!ignoreSign)
            {
                if (a.Sign < b.Sign)
                    return -1;
                else if (a.Sign > b.Sign)
                    return 1;
            }
            return
                CompareSize(a, b);
        }
        public static int CompareSize(MyInt a, MyInt b)
        {
            if (a.Sign == Sign.minus && b.Sign == Sign.minus)
            {
                if (a.Size < b.Size)
                    return 1;
                else if (a.Size > b.Size)
                    return -1;
            }
            else
            {
                if (a.Size < b.Size)
                    return -1;
                else if (a.Size > b.Size)
                    return 1;
            }
            return CompareDigits(a, b);
        }
        public static int CompareDigits(MyInt a, MyInt b)
        {
            var maxLen = Math.Max(a.Size, b.Size);
            for (var i = maxLen; i >= 0; i--)
            {
                if (a.Sign == Sign.minus && b.Sign == Sign.minus)
                {
                    if (a.GetByte(i) > b.GetByte(i))
                        return -1;
                    else if (a.GetByte(i) < b.GetByte(i))
                        return 1;
                }
                else
                {
                    if (a.GetByte(i) < b.GetByte(i))
                        return -1;
                    else if (a.GetByte(i) > b.GetByte(i))
                        return 1;
                }
            }
            return 0;
        }
        public static MyInt add(MyInt a, MyInt b)
        {
            var digits = new List<byte>();
            Sign znak = Sign.plus;
            MyInt num = Zero;
            if (a.Sign == Sign.plus && b.Sign == Sign.plus)
            {
                num = summa(a, b);
            }
            if (a.Sign == Sign.plus && b.Sign == Sign.minus)
            {
                if (compareTo(a.abs(), b.abs()) == 1)
                {
                    znak = Sign.plus;
                    num = minus(a, b);
                }
                if (compareTo(b.abs(), a.abs()) == 1)
                {
                    znak = Sign.minus;
                    num = minus(b, a);
                }
                if (compareTo(a.abs(), b.abs()) == 0)
                    num = minus(a, b);
            }
            if (a.Sign == Sign.minus && b.Sign == Sign.plus)
            {
                if (compareTo(a.abs(), b.abs()) == 1)
                {
                    znak = Sign.minus;
                    num = minus(a, b);
                }
                if (compareTo(a.abs(), b.abs()) == -1)
                {
                    znak = Sign.plus;
                    num = minus(b, a);
                }
                if (compareTo(a.abs(), b.abs()) == 0)
                    num = minus(b, a);
            }
            if (a.Sign == Sign.minus && b.Sign == Sign.minus)
            {
                znak = Sign.minus;
                num = summa(a, b);

            }
            for (int i = 0; i < num.Size; i++)
            {
                digits.Add(num.GetByte(i));
            }

            return new MyInt(znak, digits);
        }
        public static MyInt summa(MyInt a, MyInt b)
        {
            var digits = new List<byte>();
            var maxLen = Math.Max(a.Size, b.Size);
            byte t = 0;
            for (int i = 0; i < maxLen; i++)
            {
                byte sum = (byte)(a.GetByte(i) + b.GetByte(i) + t);
                if (sum > 10)
                {
                    sum -= 10;
                    t = 1;
                }
                else
                    t = 0;
                digits.Add(sum);
            }
            if (t > 0)
                digits.Add(t);
            return new MyInt(digits);
        }
        public static MyInt substract(MyInt a, MyInt b)
        {
            var digits = new List<byte>();
            Sign znak = Sign.plus;
            MyInt num = Zero;
            if (a.Sign == Sign.plus && b.Sign == Sign.plus)
            {
                if (compareTo(a, b, ignoreSign: true) == 1)
                {
                    num = minus(a, b);
                    znak = Sign.plus;
                }
                if (compareTo(a, b, ignoreSign: true) == -1)
                {
                    num = minus(b, a);
                    znak = Sign.minus;
                }
            }
            if (a.Sign == Sign.plus && b.Sign == Sign.minus)
            {
                num = summa(a, b);
            }
            if (a.Sign == Sign.minus && b.Sign == Sign.plus)
            {
                num = summa(a, b);
                znak = Sign.minus;
            }
            if (a.Sign == Sign.minus && b.Sign == Sign.minus)
            {
                if (compareTo(a.abs(), b.abs()) == 1)
                {
                    znak = Sign.minus;
                    num = minus(a, b);
                }
                if (compareTo(a.abs(), b.abs()) == -1)
                {
                    znak = Sign.plus;
                    num = minus(b, a);
                }

            }
            if (compareTo(a, b) == 0)
                digits.Add(0);
            for (int i = 0; i < num.Size; i++)
            {
                digits.Add(num.GetByte(i));
            }
            return new MyInt(znak, digits);
        }
        public static MyInt minus(MyInt a, MyInt b)
        {
            var digits = new List<byte>();
            var maxLen = Math.Max(a.Size, b.Size);
            var t = 0;
            for (var i = 0; i < maxLen; i++)
            {
                var s = a.GetByte(i) - b.GetByte(i) - t;
                if (s < 0)
                {
                    s += 10;
                    t = 1;
                }
                else
                    t = 0;
                digits.Add((byte)s);
            }
            return new MyInt(digits);
        }
        public static MyInt Zero => new MyInt(0);
        public static MyInt One => new MyInt(1);


        public static MyInt multiply(MyInt a, MyInt b)
        {
            MyInt value = Zero;
            for (var i = 0; i < a.Size; i++)
            {
                for (int j = 0, c = 0; j < b.Size || c > 0; j++)
                {
                    var current = value.GetByte(i + j) + a.GetByte(i) * b.GetByte(j) + c;
                    value.SetByte(i + j, (byte)(current % 10));
                    c = current / 10;
                }
            }
            value.Sign = a.Sign == b.Sign ? Sign.plus : Sign.minus;
            value.RemoveNulls();
            return value;

        }
        public static MyInt expVal(byte val, int exp)
        {
            MyInt myInt = Zero;
            myInt.SetByte(exp, val);
            myInt.RemoveNulls();
            return myInt;
        }
        public static MyInt divide(MyInt a, MyInt b)
        {
            var value = Zero;
            var current = Zero;
            for (var i = a.Size - 1; i >= 0; i--)
            {
                current += expVal(a.GetByte(i), i);
                var x = 0;
                var l = 0;
                var r = 10;
                while (l <= r)
                {
                    var m = (l + r) / 2;
                    var cur = b * expVal((byte)m, i);
                    if (compareTo(cur, current, ignoreSign: true) <= 0)
                    {
                        x = m;
                        l = m + 1;
                    }
                    else
                        r = m - 1;
                }
                value.SetByte(i, (byte)(x % 10));
                var t = b * expVal((byte)x, i);
                current = current - t;
            }
            value.RemoveNulls();
            value.Sign = a.Sign == b.Sign ? Sign.plus : Sign.minus;
            return value;
        }
        public static MyInt operator *(MyInt a, MyInt b) => multiply(a, b);
        public static MyInt operator +(MyInt a, MyInt b) => a.Sign == b.Sign ? add(a, b) : substract(a, b);
        public static MyInt operator -(MyInt a, MyInt b) => substract(a, b);
        public static MyInt max(MyInt a, MyInt b, bool ignoreSign = false)
        {
            if (compareTo(a, b, ignoreSign) == 1)
                return a;
            if (compareTo(a, b, ignoreSign) == -1)
                return b;
            return Zero;
        }
        public static MyInt min(MyInt a, MyInt b, bool ignoreSign = false)
        {
            if (compareTo(a, b, ignoreSign) == 1)
                return b;
            if (compareTo(a, b, ignoreSign) == -1)
                return a;
            return a;
        }
        public MyInt abs()
        {
            return new MyInt(digits);
        }
        public static MyInt gcd(MyInt a, MyInt b)
        {
            MyInt val1 = a.abs();
            MyInt val2 = b.abs();

            if (compareTo(b.abs(), Zero) != 0)
            {
                while (compareTo(a.abs(), b.abs()) != 0)
                {
                    if (compareTo(a.abs(), b.abs()) == 1)
                        a = a.abs() - b.abs();
                    else if (compareTo(a.abs(), b.abs()) == -1)
                        b = b.abs() - a.abs();
                }
            }
            return a;
        }
        public long longValue()
        {
            var result = new StringBuilder(Sign == Sign.plus ? "" : "-");

            int j = digits.Count - 19;
            digits.Reverse();
            digits.RemoveRange(1, j);
            for (var i = digits.Count - 1; i >= 0; i--)
                result.Append(Convert.ToString(digits[i]));
            long res = 0;
            res = long.Parse(result.ToString());

            return res;
        }
    }
}
