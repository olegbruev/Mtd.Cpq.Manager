using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mtd.Cpq.Manager.Data
{
    public partial class MtdCpqImportParam
    {
        public string Id { get; set; }        
        public int ColNumber { get; set; }        
        public int ColName { get; set; }
        public int ColNote { get; set; }
        public int ColData { get; set; }
        public int ColPrice { get; set; }
        public string TagData { get; set; }
        public string TagMaster { get; set; }
        public string TagRequired { get; set; }        
    }
}
