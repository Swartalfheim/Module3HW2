using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3HW2
{
    public class PhoneRecord : IComparable<PhoneRecord>
    {
        public PhoneRecord(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }

        public string Name { get; set; }
        public string Phone { get; set; }
        public int CompareTo(PhoneRecord? other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
