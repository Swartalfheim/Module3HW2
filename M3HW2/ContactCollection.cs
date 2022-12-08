using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3HW2
{
    public class ContactCollection : IContactCollection<string, PhoneRecord>
    {
        private Dictionary<string, List<PhoneRecord>> _data;
        static ContactCollection()
        {
            InitCultures();
        }

        public ContactCollection(CultureInfo culture)
        {
            DefaultCulture = new CultureInfo("en-US");
            try
            {
                if (Cultures.ContainsKey(culture.Name))
                {
                    Culture = culture;
                }
                else
                {
                    throw new Exception($"Current culture ({culture.Name}) isn't supported. Culture is set to default: {DefaultCulture.Name}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Culture = DefaultCulture;
            }

            InitDictionary();
            InitTestData();
        }

        public static Dictionary<string, List<string>> Cultures { get; private set; }
        public CultureInfo DefaultCulture { get; private set; }
        public CultureInfo Culture { get; private set; }
        public void Add(PhoneRecord item)
        {
            string section = GetSection(item.Name[0].ToString().ToUpper());
            _data[section].Add(item);
            _data[section].Sort();
        }

        public void Delete(PhoneRecord item)
        {
            _data[GetSection(item.Name[0].ToString().ToUpper())].Remove(item);
        }

        public IEnumerator<KeyValuePair<string, PhoneRecord>> GetEnumerator()
        {
            foreach (KeyValuePair<string, List<PhoneRecord>> pair in _data)
            {
                List<PhoneRecord> list = pair.Value;
                foreach (PhoneRecord item in list)
                {
                    yield return new KeyValuePair<string, PhoneRecord>(pair.Key, item);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static void InitCultures()
        {
            Cultures = new Dictionary<string, List<string>>();
            Cultures.Add("en-US", new List<string>());
            Cultures.Add("uk-UA", new List<string>());

            // init en alphabet
            for (int i = 65; i <= 90; i++)
            {
                Cultures["en-US"].Add(((char)i).ToString());
            }

            // init ua alphabet
            for (int i = 1040; i <= 1071; i++)
            {
                switch (i)
                {
                    case 1044:
                        Cultures["uk-UA"].Add(new Rune(1168).ToString()); // Ґ
                        break;
                    case 1046:
                        Cultures["uk-UA"].Add(new Rune(1028).ToString()); // Є
                        break;
                    case 1049:
                        Cultures["uk-UA"].Add(new Rune(1030).ToString()); // І
                        Cultures["uk-UA"].Add(new Rune(1031).ToString()); // Ї
                        break;
                    case 1066: // Ё, Ъ, Ы
                    case 1067:
                    case 1069:
                        continue;
                }

                Cultures["uk-UA"].Add(new Rune(i).ToString());
            }
        }

        private void InitDictionary()
        {
            _data = new Dictionary<string, List<PhoneRecord>>();
            _data.Add("#", new List<PhoneRecord>());
            _data.Add("0-9", new List<PhoneRecord>());
            foreach (string symbol in Cultures[Culture.Name])
            {
                _data.Add(symbol, new List<PhoneRecord>());
            }
        }

        private string GetSection(string symbol)
        {
            if (int.TryParse(symbol, out _))
            {
                return "0-9";
            }

            if (Cultures[Culture.Name].Contains(symbol))
            {
                return symbol;
            }

            return "#";
        }

        private void InitTestData()
        {
            switch (Culture.Name)
            {
                case "en-US":
                    Add(new PhoneRecord("Aboba", "0660000001"));
                    Add(new PhoneRecord("Eggv,k", "0660000002"));
                    Add(new PhoneRecord("Eg,kee", "0660000003"));
                    Add(new PhoneRecord("f,mjhnb", "0660000004"));
                    Add(new PhoneRecord("_sgfgjhh", "0660000005"));
                    Add(new PhoneRecord("+ftgjjkt", "0660000006"));
                    Add(new PhoneRecord("*adrfhj", "0660000008"));
                    Add(new PhoneRecord("5640gfjmh", "0660000009"));
                    Add(new PhoneRecord("21gh", "0660000010"));
                    break;
                case "uk-UA":
                    Add(new PhoneRecord("Абьрт", "0980000001"));
                    Add(new PhoneRecord("Еачпо", "0980000002"));
                    Add(new PhoneRecord("Еевсмр", "0980000003"));
                    Add(new PhoneRecord("Ебапрло", "0980000004"));
                    Add(new PhoneRecord("_укгчапо", "0980000005"));
                    Add(new PhoneRecord("+ілцапо", "0980000006"));
                    Add(new PhoneRecord("*цзчпраіябж", "0980000008"));
                    Add(new PhoneRecord("4555івоіві", "0980000009"));
                    Add(new PhoneRecord("36овчмпф", "0980000010"));
                    break;
            }
        }
    }
}
