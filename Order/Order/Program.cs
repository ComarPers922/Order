using System;
using System.Linq;

using static System.Console;
using static System.Convert;

namespace Order
{
    class Program
    {
        static void Main(string[] args)
        {
            MyTest[] m = {new MyTest("01/106") ,new MyTest("01/107"),new MyTest("01/1"),new MyTest("01/02"),new MyTest("02/2"),
                new MyTest("01/11.c"), new MyTest("01/11.b"),
                new MyTest("TN248/5"),new MyTest("AN248/4"),
                new MyTest("01/11.a/C.7"),new MyTest("01/11.a/C.1"),new MyTest("01/11.a/B.8"),new MyTest("01/11.b/B.8"), new MyTest("01/11.a"),new MyTest("01/11.b/C.7"),new MyTest("O1/11/C.7"),new MyTest("O1/11/C.1"),new MyTest("O1/11/C.47"),new MyTest("YN710/20"),
                new MyTest("TN248/45"),new MyTest("01/100"),new MyTest("01/10"),new MyTest("03/10"),new MyTest("02/12"),new MyTest("2/12"),new MyTest("12/12")};
            //MyTest[] m = { new MyTest("01/1"),new MyTest("01/10")};
            var v = from d in m orderby d descending select d;
            foreach (var item in v)
            {
                WriteLine(item.TheText);
            }
            ReadKey();
        }
    }
    class MyTest : IComparable<MyTest>
    {
        public string TheText = "";
        public MyTest(string text)
        {
            TheText = text;
        }
        public int CompareTo(MyTest other)
        {
            int i = 0;
            string[] t1 = TheText.Split('/');
            string[] t2 = other.TheText.Split('/');
            int returnValue = 0;
            for (i = 0; i < t1.Length; i++)
            {
                if(returnValue!=0)
                {
                    break;
                }
                try
                {
                    if (t1[i] == t2[i])
                    {
                        continue;
                    }
                    else
                    {
                        string tt1 = t1[i];
                        string tt2 = t2[i];
                        for (int j = 0; j < tt1.Length; j++)
                        {
                            if (char.IsDigit(tt1[j]) && !char.IsDigit(tt2[j]))
                            {
                                returnValue = 1;
                                break;
                            }
                            if (!char.IsDigit(tt1[j]) && char.IsDigit(tt2[j]))
                            {
                                returnValue = - 1;
                                break;
                            }
                            int x = 0;
                            int y = 0;
                            int l = 0;
                            int p = 0;
                            for (l = j; l < tt1.Length; l++)
                            {
                                if (char.IsDigit(tt1[l]))
                                {
                                    x *= 10;
                                    x += ToInt32(Convert.ToString(tt1[l]));                                   
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (p = j; p < tt2.Length; p++)
                            {
                                if (char.IsDigit(tt2[p]))
                                {
                                    y *= 10;
                                    y += ToInt32(Convert.ToString(tt2[p]));                                   
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (x > y)
                            {
                                returnValue = -1;
                                break;
                            }
                            else if (x < y)
                            {
                                returnValue = 1;
                                break;
                            }
                            if(tt1[l]>tt2[l])
                            {
                                returnValue = -1;
                                break;
                            }
                            else if (tt1[l] < tt2[l])
                            {
                                returnValue = 1;
                                break;
                            }
                            else
                            {
                                returnValue = 0;
                            }
                        }
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    returnValue = 0;
                    break;
                }        
            }
            if (returnValue == 0 && t1.Length > t2.Length)
            {
                return -1;
            }
            else if (returnValue == 0 && t1.Length < t2.Length)
            {
                return 1;
            }
            return returnValue;
        }
        #region 弃用代码
        //public static string ConvertTo(string SpecialText)
        //{
        //    var temp = SpecialText.Replace(".", ".0");
        //    temp = temp.Replace("(", "");
        //    temp = temp.Replace(")", "");
        //    temp = temp.Replace(" ", "");
        //    for (char i = 'A'; i <= 'z'; i++)
        //    {
        //        temp = temp.Replace(Convert.ToString(i), Convert.ToString(ToInt32(i)));
        //    }
        //    return temp;
        //}
        #endregion
    }
}
