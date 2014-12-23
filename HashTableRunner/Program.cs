using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using HashTables;

namespace HashTableRunner
{
    class Program
    {
        static ChainingHashTable<int, int> _map = new ChainingHashTable<int, int>();
        static Random _rand = new Random();
        static Stopwatch _sw = new Stopwatch();


        static void Main(string[] args)
        {
            Insert(9);
            _map.Print();

            Insert(1);
            _map.Print();

            Insert(6);
            _map.Print();

            Insert(6);
            _map.Print();

            Insert(12);
            _map.Print();

            Insert(100);
            _map.Print();

            _map.Set(1, 1);
            Console.Write("First get: ");
            TimedGet(1);

            _map.Set(1337, 9001);

            TimedGet(1337);
            Insert(1000);
            TimedGet(1337);
            Insert(10000);
            TimedGet(1337);
            Insert(100000);
            TimedGet(1337);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void Insert(int n)
        {
            for (int i = 0; i < n; i++)
            {
                _map.Set(_rand.Next(), _rand.Next());
            }
        }

        static void TimedGet(int key)
        {
            _sw.Restart();
            _map.Get(key);
            _sw.Stop();
            Console.WriteLine(String.Format("Time to get {0} from {1} items: {2}", key, _map.Count, _sw.Elapsed));
        }
    }
}
