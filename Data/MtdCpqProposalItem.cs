using System;
using System.Collections.Generic;

namespace Mtd.Cpq.Manager.Data
{
    public partial class MtdCpqProposalItem
    {
        public MtdCpqProposalItem()
        {
            MtdCpqProposalAnchor = new HashSet<MtdCpqProposalAnchor>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string Datasheet { get; set; }
        public decimal Price { get; set; }
        public string IdNumber { get; set; }
        public byte[] Image { get; set; }        
        public sbyte Selected { get; set; }
        public sbyte Required { get; set; }
        public string MtdCpqProposalId { get; set; }
        public string MtdCpqProductId { get; set; }        
        public string ProposalCatalogId { get; set; }
        public int Qty { get; set; }
        public string MtdCpqProposalOneOfId { get; set; }
        public string AnchorNotice { get; set; }
        public sbyte AnchorHistory { get; set; }
        public int Sequence { get; set; }
        public  sbyte Forbidden { get; set; }

        public virtual MtdCpqProposal MtdCpqProposal { get; set; }
        public virtual MtdCpqProposalCatalog MtdCpqProposalCatalog { get; set; }
        public virtual MtdCpqProposalOneOf MtdCpqProposalOneOf { get; set; }
        public virtual ICollection<MtdCpqProposalAnchor> MtdCpqProposalAnchor { get; set; }
    }
}
