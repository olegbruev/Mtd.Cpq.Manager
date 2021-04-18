using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mtd.Cpq.Manager.Data
{
    public partial class MtdCpqProduct
    {
        public MtdCpqProduct()
        {
            MtdCpqRuleAnchorBind = new HashSet<MtdCpqRuleAnchorBind>();
            MtdCpqRuleAnchorProductAnchorNavigation = new HashSet<MtdCpqRuleAnchor>();
            MtdCpqRuleAnchorProductMasterNavigation = new HashSet<MtdCpqRuleAnchor>();
            MtdCpqRuleAvailableProductIdChildNavigation = new HashSet<MtdCpqRuleAvailable>();
            MtdCpqRuleAvailableProductIdParentNavigation = new HashSet<MtdCpqRuleAvailable>();
        }

        public string Id { get; set; }
        public string MtdCpqCatalogId { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string Datasheet { get; set; }
        public string IdNumber { get; set; }
        public sbyte Som { get; set; }
        public sbyte Trial { get; set; }
        public byte[] Image { get; set; }
        public decimal Price { get; set; }        
        public sbyte Archive { get; set; }
        public int Sequence { get; set; }

        public virtual MtdCpqCatalog MtdCpqCatalog { get; set; }
        public virtual ICollection<MtdCpqRuleAnchorBind> MtdCpqRuleAnchorBind { get; set; }
        public virtual ICollection<MtdCpqRuleAnchor> MtdCpqRuleAnchorProductAnchorNavigation { get; set; }
        public virtual ICollection<MtdCpqRuleAnchor> MtdCpqRuleAnchorProductMasterNavigation { get; set; }
        public virtual ICollection<MtdCpqRuleAvailable> MtdCpqRuleAvailableProductIdChildNavigation { get; set; }
        public virtual ICollection<MtdCpqRuleAvailable> MtdCpqRuleAvailableProductIdParentNavigation { get; set; }
    }
}
