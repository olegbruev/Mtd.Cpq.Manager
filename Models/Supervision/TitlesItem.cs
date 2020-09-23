using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mtd.Cpq.Manager.Models.Supervision
{
    public class TitlesItem
    {
        public string Id { get; set; }
        public byte[] Logo { get; set; }
        public string Name { get; set; }
        public string PreparedBy { get; set; }
        public string ContactName { get; set; }
        public string Owner { get; set; }       
    }
}
