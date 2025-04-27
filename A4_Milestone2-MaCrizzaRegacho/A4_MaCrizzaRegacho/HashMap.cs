using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4_MaCrizzaRegacho
{
    public class HashMap<K, V> : IMap <K, V>
    {

        // Static constants
        public const int DEFAULT_CAPACITY = 11;
        public const double DEFAULT_LOADFACTOR = 0.75;

        // Properties
        public Entry<K, V>[] Table { get; set; }
        public double LoadFactor { get; set; }
        public int Threshold { get; set; }
        public int Size { get; set; } 
        public int Placeholders { get; set; } 



        // Default constructor 
        public HashMap()
        {
            Table = new Entry<K, V>[DEFAULT_CAPACITY];
            LoadFactor = DEFAULT_LOADFACTOR;
            Threshold = (int)(DEFAULT_CAPACITY * DEFAULT_LOADFACTOR);
            Size = 0;
        }

        public HashMap(int initialCapacity)
        {
            if (initialCapacity <= 0)
            {
                throw new ArgumentException("Initial capacity must be greater than 0.");
            }

            Table = new Entry<K, V>[initialCapacity]; // Initialize Table to the specified size
            LoadFactor = DEFAULT_LOADFACTOR;         // Assign the default load factor
            Threshold = (int)(initialCapacity * DEFAULT_LOADFACTOR); // Calculate the threshold
            Size = 0;                                // Initialize size to 0
        }


        public HashMap(int initialCapacity, double loadFactor)
        {
            if (initialCapacity <= 0)
            {
                throw new ArgumentException("Initial capacity must be greater than 0.");
            }

            if (loadFactor <= 0 || loadFactor >= 1)
            {
                throw new ArgumentException("Load factor must be greater than 0 and less than 1.");
            }

            Table = new Entry<K, V>[initialCapacity];
            LoadFactor = loadFactor;
            Threshold = (int)(initialCapacity * loadFactor);
            Size = 0;
        }




        public bool IsEmpty()
        {
            return Size == 0;
        }

        public void Clear()
        {
            for (int i = 0; i < Table.Length; i++)
            {
                Table[i] = null;
            }
            Size = 0;
            Placeholders = 0;
            Threshold = 0;
        }





        public V Get(K key)
        {
            int bucket = GetMatchingOrNextAvailableBucket(key);
            if (bucket != -1 && Table[bucket] != null)
            {
                return Table[bucket].Value;
            }
            return default(V);
        }



        public V Put(K key, V value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "Key cannot be null.");
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Value cannot be null.");
            }

            // Check if rehashing is needed
            if (Size + Placeholders + 1 >= Threshold)
            {
                ReHash();
            }

            // Get the bucket index for the key
            int bucket = GetMatchingOrNextAvailableBucket(key);

            // If the bucket already contains the key
            if (Table[bucket] != null && Table[bucket].Key.Equals(key))
            {
                if (Table[bucket].Value == null) // Replacing a placeholder
                {
                    Placeholders--; // Decrement placeholder count
                    Size++;         // Increment size
                }

                V oldValue = Table[bucket].Value;
                Table[bucket] = new Entry<K, V>(key, value); // Update the entry
                return oldValue; // Return the old value
            }

            // If the bucket is empty or contains a placeholder
            Table[bucket] = new Entry<K, V>(key, value);
            Size++; // Increment the size for the new entry
            return default(V); // Return null for a new insert
        }






        public V Remove(K key)
        {
            if (key == null) throw new ArgumentNullException("key cannot be null"); int index = GetMatchingOrNextAvailableBucket(key);
            Entry<K, V> oldEntry = this.Table[index];
            if (oldEntry == null)
            {
                return default(V);
            }
            V oldValue = oldEntry.Value;
            if (oldValue != null)
            {
                this.Table[index].Value = default(V);
                this.Size--;
                this.Placeholders++;
            }

            return oldValue;
        }





        public void ReHash()
        {
            int newSize = ReSize(); // Calculate the new size
            Entry<K, V>[] oldTable = Table; // Store the old table
            Table = new Entry<K, V>[newSize]; // Create a new table with the new size
            Threshold = (int)(newSize * LoadFactor); // Update the threshold based on the new size
            Size = 0; // Reset the size

            // Reinsert all entries from the old table into the new table
            foreach (var entry in oldTable)
            {
                if (entry != null && entry.Value != null) // Skip entries with null values
                {
                    Put(entry.Key, entry.Value); // Use Put to reinsert entries
                }
            }
        }



        public IEnumerable<K> Keys()
        {
            List<K> keyList = new List<K>();
            for (int i = 0; i < Table.Length; i++)
            {
                if (Table[i] != null && Table[i].Value != null)
                {
                    keyList.Add(Table[i].Key);
                }
            }
            return keyList; // Return the list as IEnumerable<K>
        }


        public IEnumerable<V> Values()
        {
            List<V> valueList = new List<V>();
            for (int i = 0; i < Table.Length; i++)
            {
                if (Table[i] != null && Table[i].Value != null)
                {
                    valueList.Add(Table[i].Value);
                }
            }
            return valueList; // Return the list as IEnumerable<V>
        }







        // HELPER METHOD
        private int FindStartingBucket(K key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "Key cannot be null.");
            }

            // Calculate the hash code and compress it to fit within the table size
            return Math.Abs(key.GetHashCode()) % Table.Length;
        }




        public int GetMatchingOrNextAvailableBucket(K key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "Key cannot be null.");
            }

            int bucket = FindStartingBucket(key);
            int startBucket = bucket; // To detect infinite loops in case of a full table

            // Use linear probing to find the first null or matching entry
            while (Table[bucket] != null)
            {
                if (Table[bucket].Key.Equals(key))
                {
                    return bucket; // Return the index if the key matches
                }

                bucket = (bucket + 1) % Table.Length; // Move to the next bucket

                // If we loop back to the starting bucket, the table is full
                if (bucket == startBucket)
                {
                    break;
                }
            }

            return bucket; // Return the index of the first null entry
        }

        private int ReSize()
        {
            int newSize = Table.Length * 2 + 1;

            while (true)
            {
                if (newSize <= 1)
                {
                    newSize += 2;
                    continue;
                }

                bool isPrime = true;
                int boundary = (int)Math.Sqrt(newSize);

                for (int i = 2; i <= boundary; i++)
                {
                    if (newSize % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    return newSize;
                }

                newSize += 2; // Increment by 2 to check the next odd number
            }
        }



    }
}
