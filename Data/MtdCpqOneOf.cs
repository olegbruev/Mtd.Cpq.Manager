using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mtd.Cpq.Manager.Data
{
    public partial class MtdCpqOneOf
    {
        public MtdCpqOneOf()
        {
            MtdCpqRuleAvailable = new HashSet<MtdCpqRuleAvailable>();            
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string Color { get; set; }
        public string ImportTag { get; set; }

        public virtual ICollection<MtdCpqRuleAvailable> MtdCpqRuleAvailable { get; set; }
    }
}
