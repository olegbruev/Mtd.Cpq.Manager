using System;
using System.Collections.Generic;

namespace Mtd.Cpq.Manager.Data
{
    public partial class MtdCpqRuleAnchor
    {
        public MtdCpqRuleAnchor()
        {
            MtdCpqRuleAnchorBind = new HashSet<MtdCpqRuleAnchorBind>();
        }

        public string Id { get; set; }
        public string ProductMaster { get; set; }
        public string ProductAnchor { get; set; }
        public string Notice { get; set; }

        public virtual MtdCpqProduct ProductAnchorNavigation { get; set; }
        public virtual MtdCpqProduct ProductMasterNavigation { get; set; }
        public virtual ICollection<MtdCpqRuleAnchorBind> MtdCpqRuleAnchorBind { get; set; }
    }
}
