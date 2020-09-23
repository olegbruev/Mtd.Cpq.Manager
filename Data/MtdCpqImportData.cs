using System;
using System.Collections.Generic;

namespace Mtd.Cpq.Manager.Data
{
    public partial class MtdCpqImportData
    {
        public string Id { get; set; }
        public string MtdCpqImportId { get; set; }
        public string IdNumber { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string Datasheet { get; set; }
        public decimal Price { get; set; }
        public sbyte Required { get; set; }
        public string Parent { get; set; }
        public string TagCatalog { get; set; }
        public string TagOneOf { get; set; }
        public int Action { get; set; }             
        public sbyte MasterProduct { get; set; }
        public virtual MtdCpqImport MtdCpqImport { get; set; }
    }
}
