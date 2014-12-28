using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    public class ChainingHashTable<K, V>
    {
        private const double _alpha = 3.0;

        private int _size = 3;

        private List<KeyValuePair<K, V>>[] _table;
        private int _count = 0;

        public ChainingHashTable()
        {
            _table = new List<KeyValuePair<K, V>>[_size];
        }

        public int Count { get { return _count; } }

        private int HashFunction(K key)
        {
            return Math.Abs(key.GetHashCode()) % _size;
        }

        private bool Saturated { get { return ((double)_count / (double)_size) > _alpha; } }

        private void Rehash()
        {
            _size = (_size * 2).NextPrime();

            List<KeyValuePair<K, V>>[] newTable = new List<KeyValuePair<K, V>>[_size];

            for (int i = 0; i < _table.Length; i++)
            {
                List<KeyValuePair<K, V>> row = _table[i];

                if (row != null)
                {
                    foreach (KeyValuePair<K, V> kvp in row)
                    {
                        int location = HashFunction(kvp.Key);

                        if (newTable[location] == null)
                        {
                            newTable[location] = new List<KeyValuePair<K, V>>();
                        }

                        newTable[location].Add(kvp);
                    }
                }
            }

            _table = newTable;
        }

        public V this[K key]
        {
            get
            {
                return Get(key);
            }

            set
            {
                Set(key, value);
            }
        }

        public void Set(K key, V value)
        {
            Remove(key);
            _count++;

            if (Saturated)
            {
                Rehash();
            }

            int location = HashFunction(key);

            if (_table[location] == null)
            {
                _table[location] = new List<KeyValuePair<K, V>>();
            }

            _table[location].Add(new KeyValuePair<K, V>(key, value));
        }

        public V Get(K key)
        {
            int location = HashFunction(key);
            List<KeyValuePair<K, V>> row = _table[location];

            if (row != null)
            {
                foreach (KeyValuePair<K, V> kvp in row)
                {
                    if (kvp.Key.Equals(key))
                    {
                        return kvp.Value;
                    }
                }
            }

            return default(V);
        }

        public bool Remove(K key)
        {
            int location = HashFunction(key);
            List<KeyValuePair<K, V>> row = _table[location];

            if (row != null)
            {
                for (int i = 0; i < row.Count; i++)
                {
                    KeyValuePair<K, V> kvp = row[i];
                    if (kvp.Key.Equals(key))
                    {
                        row.RemoveAt(i);
                        _count--;
                        return true;
                    }
                }
            }

            return false;
        }

        public void Print()
        {
            Console.WriteLine("================================");
            Console.WriteLine(String.Format("Count: {0}, Size: {1}, Alpha: {2}", Count, _size, _alpha));
            for (int i = 0; i < _size; i++)
            {
                List<KeyValuePair<K, V>> row = _table[i];
                Console.Write(String.Format("{0}: ", i));

                if (row != null)
                {
                    Console.Write(String.Join(", ", row.Select(kvp => String.Format("<{0}: {1}>", kvp.Key.ToString(), kvp.Value.ToString()))));
                }

                Console.WriteLine();
            }
            Console.WriteLine("================================\n");
        }
    }
}
