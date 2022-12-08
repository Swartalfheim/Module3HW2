using System.Text;

namespace M3HW2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IContactCollection<string, PhoneRecord> contactCollection;
            string culture, previousSection = string.Empty, currentSection;

            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Available cultures: ");
            foreach (string key in ContactCollection.Cultures.Keys)
            {
                Console.WriteLine($"{key} ");
            }

            Console.WriteLine();
            Console.Write("Enter culture: ");
            culture = Console.ReadLine();
            Console.WriteLine();
            foreach (string key in ContactCollection.Cultures.Keys)
            {
                if (key.ToLower().Contains(culture.ToLower()))
                {
                    culture = key;
                    break;
                }
            }

            contactCollection = new ContactCollection(new System.Globalization.CultureInfo(culture));
            foreach (KeyValuePair<string, PhoneRecord> contact in contactCollection)
            {
                currentSection = contact.Key;
                if (previousSection != currentSection)
                {
                    if (currentSection != "#")
                    {
                        Console.WriteLine();
                    }

                    Console.WriteLine(currentSection);
                }

                Console.WriteLine($"{contact.Value.Name.PadRight(15)} {contact.Value.Phone}");
                previousSection = currentSection;
            }
        }
    }
}