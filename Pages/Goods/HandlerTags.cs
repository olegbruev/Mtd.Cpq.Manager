using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mtd.Cpq.Manager.Pages.Goods
{
    public class HandlerTags
    {
        private readonly CpqContext _context;
        private readonly string[] Data = {"A", "B", "C", "D","E","F","G","H","I","J","K","L","M","N",
            "O","P","Q","R","S","T","U","V","W","X","Y","Z","1","2","3","4","5","6","7","8","9","0","@", "#", "!", "&","?",".","$","*","(",")","-","+","="};

        public HandlerTags(CpqContext context) {
            _context = context;
        }

        public async Task<List<TagData>> GetFreeListAsync(string current=null)
        {
            List<TagData> busy = new List<TagData>();
            IList<TagData> catalogs = await _context.MtdCpqCatalog.Where(x=>x.ImportTag != null)
                .Select(x =>  new TagData { Id= x.ImportTag, Name = x.ImportTag}).ToListAsync();
            IList<TagData> oneOfs = await _context.MtdCpqOneOfs.Where(x=>x.ImportTag != null)
                .Select(x => new TagData { Id=x.ImportTag, Name = x.ImportTag }).ToListAsync();
            
            MtdCpqImportParam param = await _context.MtdCpqImportParams.FirstOrDefaultAsync();
            if (param != null && param.TagData != null)
            busy.Add(new TagData { Id=param.TagData,Name=param.TagData});
            
            if (param != null && param.TagMaster != null)
                busy.Add(new TagData { Id = param.TagMaster, Name = param.TagMaster });

            if (param != null && param.TagRequired != null)
                busy.Add(new TagData { Id = param.TagRequired, Name = param.TagRequired });

            if (catalogs.Count > 0) { busy.AddRange(catalogs); }
            if (oneOfs.Count > 0) { busy.AddRange(oneOfs); }

            List<TagData> freeList = new List<TagData>();
            List<TagData> tags = Data.Where(x => !busy.Where(s => s.Id != current).Select(s => s.Id).Contains(x)).Select(x => new TagData { Id = x, Name = x }).ToList();
            if (tags.Count > 0)
            {
                freeList.AddRange(tags);
            }
            
            return freeList;
        }

    }

    public class TagData { 
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
