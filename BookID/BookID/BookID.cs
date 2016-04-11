using System;
using System.Collections.Generic;

using static System.Convert;

namespace BookID
{
    public class MyBookID : IComparable<MyBookID>,IComparer<MyBookID>
    {
        public string ID { set; get; }
        public MyBookID(string id)
        {
            ID = id;
        }

        public int CompareTo(MyBookID other)
        {
            int i = 0;
            string thisText = ID.Replace('-', '?'); ;
            string otherText = other.ID.Replace('-', '?'); ;
            int returnValue = 0;
            int j = 0;
            if (char.IsDigit(thisText[0]) && !char.IsDigit(otherText[0]))
            {
                returnValue = 1;
                goto result;
            }
            if (!char.IsDigit(thisText[0]) && char.IsDigit(otherText[0]))
            {
                returnValue = -1;
                goto result;
            }
            try
            {
                bool fraction = false;
                int div = 10;
                for (i = 0; i < thisText.Length;)
                {
                    if (char.IsDigit(thisText[i]) && !char.IsDigit(otherText[i]))
                    {
                        returnValue = otherText[i] == '/' ? -1 : 1;
                        goto result;
                    }
                    if (!char.IsDigit(thisText[i]) && char.IsDigit(otherText[i]))
                    {
                        returnValue = thisText[i] == '/' ? 1 : -1;
                        goto result;
                    }
                    double x = 0, y = 0;
                    try
                    {
                        for (j = i; j < thisText.Length; j++)
                        {
                            if (char.IsDigit(thisText[j]))
                            {
                                if (!fraction)
                                {
                                    x *= 10;
                                    x += ToInt32(thisText[j].ToString());
                                }
                                else
                                {
                                    x += ToDouble(thisText[j].ToString()) / div;
                                    div *= 10;
                                }
                            }
                            else if (thisText[j] == '.')
                            {
                                fraction = true;
                            }
                            else
                            {
                                break;
                            }
                        }
                        div = 10;
                        fraction = false;
                        for (int k = i; k < otherText.Length; k++)
                        {
                            if (char.IsDigit(otherText[k]))
                            {
                                if (!fraction)
                                {
                                    y *= 10;
                                    y += ToInt32(otherText[k].ToString());
                                }
                                else
                                {
                                    y += ToDouble(otherText[k].ToString()) / div;
                                    div *= 10;
                                }
                            }
                            else if (otherText[k] == '.')
                            {
                                fraction = true;
                            }
                            else
                            {
                                break;
                            }
                        }
                        fraction = false;
                        div = 10;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        if (x > y)
                        {
                            returnValue = -1;
                            goto result;
                        }
                        if (x < y)
                        {
                            returnValue = 1;
                            goto result;
                        }
                        else
                        {
                            returnValue = 0;
                        }
                    }

                    if (x > y)
                    {
                        returnValue = -1;
                        goto result;
                    }
                    if (x < y)
                    {
                        returnValue = 1;
                        goto result;
                    }
                    else
                    {
                        returnValue = 0;
                    }
                    i += j - i;

                    if (thisText[i] == '-' && otherText[i] != '-')
                    {
                        returnValue = -1;
                        goto result;
                    }
                    else if (thisText[i] != '-' && otherText[i] == '-')
                    {
                        returnValue = 1;
                        goto result;
                    }

                    if (thisText[i] > otherText[i])
                    {
                        returnValue = -1;
                        goto result;
                    }
                    else if (thisText[i] < otherText[i])
                    {
                        returnValue = 1;
                        goto result;
                    }
                    else
                    {
                        returnValue = 0;
                        i++;
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                if (thisText.Length > otherText.Length)
                {
                    returnValue = -1;
                }
                if (thisText.Length < otherText.Length)
                {
                    returnValue = 1;
                }
            }
        result:
            return returnValue;
        }

        public override string ToString()
        {
            return ID;
        }

        public int Compare(MyBookID x, MyBookID y)
        {
            return x.CompareTo(y);
        }
    }
}
