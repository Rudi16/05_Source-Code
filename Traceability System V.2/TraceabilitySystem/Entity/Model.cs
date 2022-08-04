using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceabilitySystem.Entity
{
    public class Model
    {
        public int id_model{set;get;}
        public string gmccode { get; set; }
        public string model  {get;set;}
        public string color { get; set; }
        public string ean { get; set; }
        public string upc { get; set; }

    }

}
