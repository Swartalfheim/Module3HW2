using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3HW2
{
    public interface IContactCollection<K, V> : IEnumerable<KeyValuePair<K, V>>
    {
        void Add(V item);
        void Delete(V item);
    }
}
