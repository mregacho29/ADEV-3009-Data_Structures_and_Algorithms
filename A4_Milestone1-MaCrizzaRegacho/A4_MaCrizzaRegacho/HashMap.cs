using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4_MaCrizzaRegacho
{
    public class HashMap<K, V>
    {

        // Static constants
        public const int DEFAULT_CAPACITY = 11;
        public const double DEFAULT_LOADFACTOR = 0.75;

        // Properties
        public Entry<K, V>[] Table { get; set; }
        public double LoadFactor { get; set; }
        public int Threshold { get; set; }
        public int Size { get; set; }

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

        public int Placeholders
        {
            get
            {
                int placeholderCount = 0;
                foreach (var entry in Table)
                {
                    if (entry != null)
                    {
                        if (entry.Value == null)
                        {
                            placeholderCount++;
                        }
                    }
                }
                return placeholderCount;
            }
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
            Threshold = 0;
        }



        public int GetMatchingOrNextAvailableBucket(K key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "Key cannot be null.");
            }

            int bucket = GetBucketIndex(key);
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
            // Throw an exception if the key is null
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "Key cannot be null.");
            }

            // Throw an exception if the value is null
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Value cannot be null.");
            }

            // Check if rehashing is needed (actual size includes placeholders)
            if (Size + Placeholders + 1 >= Threshold)
            {
                ReHash(); // Resize and rehash the table
            }

            // Get the bucket index for the key
            int bucket = GetBucketIndex(key);

            // Use linear probing to handle collisions
            while (Table[bucket] != null)
            {
                // If the key already exists, update the value and return the old value
                if (Table[bucket].Key.Equals(key))
                {
                    if (Table[bucket].Value == null) // If it's a placeholder
                    {
                        Size++; // Increment size as we're replacing a placeholder
                    }

                    V oldValue = Table[bucket].Value; // Store the old value
                    Table[bucket] = new Entry<K, V>(key, value); // Update the entry
                    return oldValue; // Return the old value
                }
                bucket = (bucket + 1) % Table.Length; // Move to the next bucket
            }

            // Add a new entry if no matching key is found
            Table[bucket] = new Entry<K, V>(key, value);
            Size++; // Increment the size for the new entry
            return default(V); // Return null for a new insert
        }


        public V Remove(K key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "Key cannot be null.");
            }

            // Find the bucket index using GetMatchingOrNextAvailableBucket
            int bucket = GetMatchingOrNextAvailableBucket(key);

            // Check if the bucket contains the key
            if (bucket != -1 && Table[bucket] != null && Table[bucket].Key.Equals(key))
            {
                V removedValue = Table[bucket].Value; // Store the value to return
                Table[bucket] = new Entry<K, V>(key, default(V)); // Leave a placeholder with the same key but default value
                Size--; // Decrease the size
                return removedValue; // Return the removed value
            }

            return default(V); // Return default if the key is not found
        }



        private int ReSize()
        {
            int newSize = Table.Length * 2 + 1; // Double the current size and add 1
            while (!IsPrime(newSize))
            {
                newSize++; // Increment until a prime number is found
            }
            return newSize; // Return the next prime number
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






        public IEnumerable<V> Values()
        {
            foreach (var entry in Table)
            {
                if (entry != null && entry.Value != null) // Skip null entries and placeholders
                {
                    yield return entry.Value; // Return the value of the valid entry
                }
            }
        }


        public IEnumerable<K> Keys()
        {
            foreach (var entry in Table)
            {
                if (entry != null && entry.Value != null) // Skip null entries and placeholders
                {
                    yield return entry.Key; // Return the key of the valid entry
                }
            }
        }



        // HELPER METHOD
        private int GetBucketIndex(K key)
        {
            return Math.Abs(key.GetHashCode()) % Table.Length;
        }

        private bool IsPrime(int number)
        {
            if (number <= 1) return false; // Numbers less than or equal to 1 are not prime
            if (number <= 3) return true;  // 2 and 3 are prime numbers
            if (number % 2 == 0 || number % 3 == 0) return false; // Eliminate multiples of 2 and 3

            // Check divisors from 5 to √number
            for (int i = 5; i * i <= number; i += 6)
            {
                if (number % i == 0 || number % (i + 2) == 0)
                {
                    return false;
                }
            }
            return true;
        }


    }
}
