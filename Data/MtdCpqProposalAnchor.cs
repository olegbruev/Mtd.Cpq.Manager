using System;
using System.Collections.Generic;

namespace Mtd.Cpq.Manager.Data
{
    public partial class MtdCpqProposalAnchor
    {
        public string Id { get; set; }
        public string Cid { get; set; }
        public string MtdCpqProductId { get; set; }
        public sbyte Include { get; set; }
        public sbyte Required { get; set; }
        public virtual MtdCpqProposalItem MtdCpqProposalItem { get; set; }
    }
}
