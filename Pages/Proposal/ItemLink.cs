using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mtd.Cpq.Manager.Pages.Proposal
{
    public class ItemLink
    {
        private List<MtdCpqProposalAnchor> anchors;
        private List<MtdCpqProposalItem> items;

        private readonly CpqContext context;

        public ItemLink(CpqContext context)
        {
            this.context = context;
        }
        
        public void SetItems(List<MtdCpqProposalItem> items)
        {
            this.items = items;
        }

        public void SetIAnchors(List<MtdCpqProposalAnchor> anchors)
        {
            this.anchors = anchors;
        }

        public async Task<List<MtdCpqProposalItem>> CheckAnchorsAsync(string item_id, MtdCpqProposal proposal, sbyte selected)
        {
            if (items == null)
            {
                items = await context.MtdCpqProposalItem.AsNoTracking().Where(x => x.MtdCpqProposalId == proposal.Id).ToListAsync();
            }

            if (anchors == null)
            {
                List<string> ids = items.Select(x => x.Id).ToList();
                anchors = await context.MtdCpqProposalAnchor.AsNoTracking().Where(x => ids.Contains(x.Cid)).ToListAsync();
            }

            List<MtdCpqProposalItem> bindItems = new List<MtdCpqProposalItem>();
            List<string> includeIds = anchors.Where(x => x.Include == 1 && x.Required == 0 && x.Cid == item_id).Select(x => x.MtdCpqProductId).ToList();
            List<string> excludeIds = anchors.Where(x => x.Include == 0 && x.Required == 0 && x.Cid == item_id).Select(x => x.MtdCpqProductId).ToList();
            List<string> requiredIn = anchors.Where(x => x.Include == 1 && x.Required == 1 && x.Cid == item_id).Select(x => x.MtdCpqProductId).ToList();
            List<string> requiredEx = anchors.Where(x => x.Include == 0 && x.Required == 1 && x.Cid == item_id).Select(x => x.MtdCpqProductId).ToList();

            List<string> allChildIds = new List<string>();
            allChildIds.AddRange(includeIds);
            allChildIds.AddRange(excludeIds);
            allChildIds.AddRange(requiredIn);
            allChildIds.AddRange(requiredEx);

            if (allChildIds.Count > 0 && proposal.ConfigChangeRule == 0)
            {
                bindItems = items.Where(x => x.MtdCpqProposalId == proposal.Id && allChildIds.Contains(x.MtdCpqProductId)).ToList();

                foreach (var bindItem in bindItems)
                {
                    if (selected == 1)
                    {
                        bindItem.AnchorHistory = bindItem.Required;
                        bindItem.Required = requiredIn.Contains(bindItem.MtdCpqProductId) ? (sbyte)1 : (sbyte)0;
                        bindItem.Forbidden = requiredEx.Contains(bindItem.MtdCpqProductId) ? (sbyte)1 : (sbyte)0;
                        if (bindItem.Forbidden == 1 || excludeIds.Contains(bindItem.MtdCpqProductId))
                        {
                            bindItem.Selected = 0;
                        }
                        else
                        {
                            bindItem.Selected = 1;
                        }
                    }
                    else
                    {
                        bindItem.Required = bindItem.AnchorHistory;
                        bindItem.Selected = bindItem.Required;
                        bindItem.Forbidden = 0;
                    }
                }
            }

            return bindItems;
        }

    }
}
