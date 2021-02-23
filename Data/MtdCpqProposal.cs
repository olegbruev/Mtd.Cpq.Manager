using System;
using System.Collections.Generic;

namespace Mtd.Cpq.Manager.Data
{
    public partial class MtdCpqProposal
    {
        public MtdCpqProposal()
        {
            MtdCpqProposalItem = new HashSet<MtdCpqProposalItem>();
            MtdCpqProposalOneOf = new HashSet<MtdCpqProposalOneOf>();
        }

        public string Id { get; set; }
        public string IdNumber { get; set; }
        public DateTime DateCreation { get; set; }
        public string TitleName { get; set; }
        public string TitleNote { get; set; }
        public string PreparedFor { get; set; }
        public string PreparedBy { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string Description { get; set; }
        public byte[] Logo { get; set; }
        public int LogoWidth { get; set; }
        public int LogoHeight { get; set; }
        public sbyte LogoFlexible { get; set; }
        public decimal PriceCustomer { get; set; }
        public string CustomerCurrency { get; set; }        
        public sbyte ConfigMasterInluded { get; set; }
        public sbyte ConfigChangeRule { get; set; }
        public string DeliveryCondition { get; set; }
        public string MasterId { get; set; }
        public string MasterNumber { get; set; }
        public string MasterName { get; set; }
        public string MasterNote { get; set; }
        public string MasterDatasheet { get; set; }
        public byte[] MasterImage { get; set; }
        public decimal MasterPrice { get; set; }
        public int MasterQty { get; set; }
        public sbyte ViewNote { get; set; }
        public sbyte ViewPriceGross { get; set; }
        public sbyte ViewPriceCustomer { get; set; }
        public sbyte ViewDelivery { get; set; }
        public sbyte ViewQty { get; set; }
        public sbyte ViewImages { get; set; }
        public sbyte ViewProposal { get; set; }
        public sbyte ViewDatasheet { get; set; }
        public string Language { get; set; }

        public virtual ICollection<MtdCpqProposalItem> MtdCpqProposalItem { get; set; }
        public virtual ICollection<MtdCpqProposalCatalog> MtdCpqProposalCatalog { get; set; }
        public virtual ICollection<MtdCpqProposalOneOf> MtdCpqProposalOneOf { get; set; }
    }
}
