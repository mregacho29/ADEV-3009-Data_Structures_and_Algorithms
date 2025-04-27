using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace A4_MaCrizzaRegacho
{
    public class Entry<K, V>
    {
        /// <summary>
        /// Gets the key of the entry.
        /// </summary>
        public K Key { get; set; }

        /// <summary>
        /// Gets the value of the entry.
        /// </summary>
        public V Value { get; set; }

        /// <summary>
        /// Constructor to initialize the Entry with a key and a value.
        /// </summary>
        /// <param name="key">The key of the entry.</param>
        /// <param name="value">The value of the entry.</param>
        public Entry(K key, V value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Overrides the ToString method to return a human-readable representation of the entry.
        /// </summary>
        /// <returns>A string in the format "[key : value]".</returns>
        public override string ToString()
        {
            return $"[{Key} : {Value}]";
        }
    }
}