using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG___Task_1
{
    internal class Books : IComparable<Books>, IEqualityComparer<Books>
    {
        //Variables declared
        public double CallNum { get; set; }
        public string Author { get; set; }

        public Books()
        {

        }

        //Constructor
        public Books(double callNum, string author)
        {
            this.CallNum = callNum;
            this.Author = author;
        }

        //Comparer Class
        public int CompareTo(Books bk)
        {
            int results = CallNum.CompareTo(bk.CallNum);
            if (results == 0)
            {
                results = Author.CompareTo(bk.Author);
            }

            return results;
        }

        //Boolean class
        public bool Equals(Books x, Books y)
        {
            if (x.CallNum.Equals(y.CallNum) && x.Author.Equals(y.Author))
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(Books bk)
        {
            return bk.GetHashCode();
        }

    }
}
