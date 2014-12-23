using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    public static class IntegerExtensions
    {
        public static bool IsPrime(this int n)
        {
            for (int i = 2; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static int NextPrime(this int n)
        {
            do
            {
                n++;
            }
            while (!n.IsPrime());

            return n;
        }
    }
}
