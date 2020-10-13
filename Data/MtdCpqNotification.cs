using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mtd.Cpq.Manager.Data
{
    public class MtdCpqNotification
    {        

        public MtdCpqNotification()
        {
            MtdCpqReaderUsers = new HashSet<MtdCpqReaderUser>();
        }

        public string Id { get; set; }
        public string  Message { get; set; }
        public string UserId { get; set; }
        public DateTime TimeCr { get; set; }

        public virtual ICollection<MtdCpqReaderUser> MtdCpqReaderUsers { get; set; }

    }
}
