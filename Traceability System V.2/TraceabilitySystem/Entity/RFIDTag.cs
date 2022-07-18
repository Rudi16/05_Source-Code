using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceabilitySystem.Entity
{
    public class RFIDTag
    {
        public int ID { set; get; }
        public string SerialRFID { set; get; }
        public string EPCNumber { set; get; }
        public DateTime LastUpdate  { set; get; }
        public string Pic { set; get; }
    }
}
 