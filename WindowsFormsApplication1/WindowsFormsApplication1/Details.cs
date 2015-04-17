using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Details
    {
        public string type { get; set; }
        public string weight_class { get; set; }
        public string defense { get; set; }
        public int min_power { get; set; }
        public int max_power { get; set; }
        public string damage_type { get; set; }
        public Infix infix_upgrade { get; set; }
        public string [] bonuses { get; set; }
    }
}
