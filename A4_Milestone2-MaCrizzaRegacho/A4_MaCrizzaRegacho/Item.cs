using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace A4_MaCrizzaRegacho
{
    public class Item : IComparable<Item>
    {
        /// <summary>
        /// The name of the item. Properties are set in the constructor.
        /// </summary>
        public string Name { get; set; }
        public int GoldPieces { get; set; }
        public double Weight { get; set; }


        /// <summary>
        /// Initializes a new instance of the Item class with the specified name, gold value, and weight.
        /// </summary>
        /// <param name="name">The name of the item.</param>
        /// <param name="goldPieces">The value of the item in gold pieces.</param>
        /// <param name="weight">The weight of the item in kilograms.</param>
        public Item(string name, int goldPieces, double weight)
        {
            Name = name;
            GoldPieces = goldPieces;
            Weight = weight;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current Item class
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is Item other &&
                   Name == other.Name &&
                   GoldPieces == other.GoldPieces &&
                   Weight == other.Weight;
        }

        /// <summary>
        /// Compares the current <see cref="Item"/> with another <see cref="Item"/> based on their names.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Item other)
        {
            return string.Compare(Name, other?.Name, StringComparison.Ordinal);
        }


        /// <summary>
        /// Returns a string representation of the current <see cref="Item"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name} is worth {GoldPieces}gp and weighs {Weight}kg";
        }

    }
}
