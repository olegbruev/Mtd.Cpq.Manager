using System;
using System.Collections.Generic;

namespace Mtd.Cpq.Manager.Data
{
    public partial class MtdCpqRuleAvailable
    {
        public string Id { get; set; }
        public string ProductIdParent { get; set; }
        public string ProductIdChild { get; set; }
        public sbyte Required { get; set; }
        public string OneOfId { get; set; }
        public virtual MtdCpqProduct ProductIdChildNavigation { get; set; }
        public virtual MtdCpqProduct ProductIdParentNavigation { get; set; }
        public virtual MtdCpqOneOf MtdCpqOneOf { get; set; }
    }
}
