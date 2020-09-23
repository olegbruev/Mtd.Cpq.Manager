using System;
using System.Collections.Generic;

namespace Mtd.Cpq.Manager.Data
{
    public partial class MtdCpqRuleAnchorBind
    {
        public string Id { get; set; }
        public string MtdCpqRuleAnchorId { get; set; }
        public string MtdCpqProductId { get; set; }
        public sbyte Include { get; set; }
        public sbyte Required { get; set; }
        public virtual MtdCpqProduct MtdCpqProduct { get; set; }
        public virtual MtdCpqRuleAnchor MtdCpqRuleAnchor { get; set; }
    }
}
