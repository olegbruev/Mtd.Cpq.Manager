using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mtd.Cpq.Manager.Data
{
    public class MtdCpqReaderUser
    {
        public string Id { get; set; }
        public string MessageId { get; set; }
        public string UserName { get; set; }
        public DateTime TimeCr { get; set; }

        public virtual MtdCpqNotification MtdCpqNotification { get; set; }

    }
}
