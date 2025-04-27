using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4_MaCrizzaRegacho
{
    interface IMap<K, V>
    {
        int Size { get; } // Property to get the number of entries (excluding placeholders)
        bool IsEmpty(); // Check if the map is empty
        void Clear(); // Clear all entries in the map
        V Get(K key); // Retrieve the value associated with the specified key
        V Put(K key, V value); // Add or update an entry in the map
        V Remove(K key); // Remove the value associated with the specified key
    }
}
