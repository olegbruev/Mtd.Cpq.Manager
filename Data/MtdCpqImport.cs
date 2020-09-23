using System;
using System.Collections.Generic;

namespace Mtd.Cpq.Manager.Data
{
    public partial class MtdCpqImport
    {
        public MtdCpqImport()
        {
            MtdCpqImportData = new HashSet<MtdCpqImportData>();
        }

        public string Id { get; set; }
        public DateTime TimeCr { get; set; }
        public string UserId { get; set; }
        public string StatusText { get; set; }
        public int StatusProcess { get; set; }
        public sbyte NoteLoad {get;set;}
        public sbyte OldToArchive { get; set; }
        public sbyte DatasheetLoad { get; set; }

        public virtual ICollection<MtdCpqImportData> MtdCpqImportData { get; set; }
    }
}
