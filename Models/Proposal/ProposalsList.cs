using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mtd.Cpq.Manager.Models.Proposal
{
    public class ProposalsList
    {
        public string Id { get; set; }
        public string Date { get; set; }
        public string Number { get; set; }
        public byte[] Logo { get; set; }
        public byte[] MasterImage { get; set; }
        public string MasterNumber { get; set; }
        public string MasterName { get; set; }
        public string PreparedFor { get; set; }
        public string Description { get; set; }
        public string ContactName { get; set; }
        public string OwnerName { get; set; }
        public string OwnerGroup { get; set; }
    }
}
