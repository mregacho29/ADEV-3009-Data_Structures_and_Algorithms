using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4_MaCrizzaRegacho
{
    public class Adventure
    {
        public HashMap<StringKey, Item> BackPack; // Declare the BackPack field



        public Adventure(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName), "File name cannot be null.");
            }

            if (!File.Exists(fileName))
            {
                throw new ArgumentException("The specified file does not exist.", nameof(fileName));
            }

            BackPack = new HashMap<StringKey, Item>();

            foreach (var line in File.ReadLines(fileName))
            {
                var parts = line.Split(',');
                if (parts.Length == 3)
                {
                    string name = parts[0].Trim();
                    int goldPieces = int.Parse(parts[1].Trim());
                    double weight = double.Parse(parts[2].Trim());

                    var item = new Item(name, goldPieces, weight);
                    BackPack.Put(new StringKey(name), item);
                }
            }
        }


        public string PrintLootMap()
        {
            var result = new List<string>();

            foreach (var item in BackPack.Values())
            {
                if (item.GoldPieces > 0) // Filter out items with 0 GoldPieces
                {
                    result.Add(item.ToString());
                }
            }

            result.Sort(); // Sort alphabetically
            return string.Join("\n", result) + "\n";
        }

    }
}
