using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Item
    {
        public string name { get; set; }
        public string description { get; set; }
        public string rarity { get; set; }
        public int id { get; set; }
        public string type { get; set; }
        public int vendor_value { get; set; }
        public int level { get; set; }
        public string[] game_types { get; set; }
        public string[] restrictions { get; set; }
        public Details details  { get; set;}

    }
}
