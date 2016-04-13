using System;
using System.Collections.Generic;
using BookID;

namespace AdvBookID
{
    public class MyAdvBookID:IComparable<MyAdvBookID>,IComparer<MyAdvBookID>
    {
        public string TheText = "";

        public MyAdvBookID(string text)
        {
            TheText = text;
        }
        public int Compare(MyAdvBookID x, MyAdvBookID y)
        {
            return x.CompareTo(y);
        }
        public int CompareTo(MyAdvBookID other)
        {
            int returnValue = 0;
            string thisText = TheText.Split('/')[0];
            string otherText = other.TheText.Split('/')[0];

            try
            {
                for (int i = 0; i < thisText.Length; i++)
                {
                    if (thisText[i] > otherText[i])
                    {
                        returnValue = -1;
                        break;
                    }
                    else if (thisText[i] < otherText[i])
                    {
                        returnValue = 1;
                        break;
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                returnValue = -1;
            }
            if (returnValue == 0 && otherText.Length > thisText.Length)
            {
                returnValue = 1;
            }
            returnValue = (returnValue == 0 ? (new MyBookID(TheText)).CompareTo(new MyBookID(other.TheText)) : returnValue);
            return returnValue;
        }

        public override string ToString()
        {
            return TheText;
        }
    }
}
