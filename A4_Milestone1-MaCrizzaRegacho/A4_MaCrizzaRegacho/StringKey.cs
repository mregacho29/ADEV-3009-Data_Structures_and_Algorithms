using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4_MaCrizzaRegacho
{
    public class StringKey : IComparable<StringKey>
    {
        /// <summary>
        /// Gets or sets the name of the Key
        /// </summary>
        public string KeyName { get; set; }


        /// <summary>
        /// Constructor for the StringKey class that initializes the KeyName property.
        /// </summary>
        /// <param name="KeyName"></param>
        public StringKey(String KeyName)
        {
            this.KeyName = KeyName;

        }


        /// <summary>
        /// Overrides the Equals method to compare two StringKey objects based on their KeyName property.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            // Check if the object is null or not of the same type
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // Cast the object to StringKey and compare the KeyName property
            StringKey other = (StringKey)obj;
            return KeyName == other.KeyName;
        }



        /// <summary>
        /// Compares this instance with another StringKey instance.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(StringKey other)
        {
            return string.Compare(KeyName, other?.KeyName ?? string.Empty, StringComparison.Ordinal);
        }


        /// <summary>
        /// Overrides the ToString method to return a string representation of the StringKey object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"KeyName: {KeyName} HashCode: {GetHashCode()}";
        }


        /// <summary>
        /// Overrides the GetHashCode method to generate a hash code based on the KeyName property.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (string.IsNullOrEmpty(KeyName)) return 0; // Return 0 for empty or null KeyName

            int hash = 0;
            int coefficient = 31; // Coefficient as per the theory slides
            int power = 1; // Start with coefficient^0, which is 1

            foreach (char c in KeyName)
            {
                hash += c * power; // Multiply character by the current power of the coefficient
                power *= coefficient; // Update power to the next coefficient^n
            }

            return Math.Abs(hash); // Ensure the hash code is non-negative
        }



    }
}
