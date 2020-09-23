using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mtd.Cpq.Manager.Data
{
    public partial class MtdCpqProposalOneOf
    {
        public MtdCpqProposalOneOf()
        {
            MtdCpqProposalItem = new HashSet<MtdCpqProposalItem>();
        }

        public string Id { get; set; }
        public string CId { get; set; }
        public string MtdCpqProposalId { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string Color { get; set; }

        public virtual ICollection<MtdCpqProposalItem> MtdCpqProposalItem { get; set; }
        public virtual MtdCpqProposal MtdCpqProposal { get; set; }
    }
}
