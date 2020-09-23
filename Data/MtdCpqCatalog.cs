using System;
using System.Collections.Generic;

namespace Mtd.Cpq.Manager.Data
{
    public partial class MtdCpqCatalog
    {
        public MtdCpqCatalog()
        {
            MtdCpqProduct = new HashSet<MtdCpqProduct>();
        }

        public string Id { get; set; }
        public string IdNumber { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int Sequence { get; set; }
        public byte[] Image { get; set; }
        public string ImportTag { get; set; }

        public virtual ICollection<MtdCpqProduct> MtdCpqProduct { get; set; }
    }
}
