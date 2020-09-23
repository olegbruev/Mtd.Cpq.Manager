using Microsoft.EntityFrameworkCore;
using Mtd.Cpq.Manager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mtd.Cpq.Manager.Pages.Goods.Import.Handler
{

    public class ImportHandler
    {
        private readonly CpqContext _context;

        private string parentNumber;

        private MtdCpqImport importInfo;
        private MtdCpqImportParam importParam;
        private List<MtdCpqImportData> importData;
        private List<MtdCpqCatalog> tagsCatalog;
        private List<MtdCpqOneOf> tagsOneOfs;
        private readonly string user;
        private bool _noteUpdate;
        private bool _dataUpdate;
        private bool _oldToArchive;

        public ImportHandler(CpqContext context, string id_user)
        {
            user = id_user;
            _context = context;
        }
        
        public async Task StartImportAsync(string importId, bool noteUpdate, bool dataUpdate, bool oldToArchive = false)
        {
            _noteUpdate = noteUpdate;
            _oldToArchive = oldToArchive;
            _dataUpdate = dataUpdate;
            importParam = await _context.MtdCpqImportParams.AsNoTracking().FirstOrDefaultAsync();
            tagsCatalog = await _context.MtdCpqCatalog.AsNoTracking().Select(x => new MtdCpqCatalog { Id = x.Id, ImportTag = x.ImportTag }).ToListAsync();
            tagsOneOfs = await _context.MtdCpqOneOfs.AsNoTracking().Select(x => new MtdCpqOneOf { Id = x.Id, ImportTag = x.ImportTag }).ToListAsync();
            parentNumber = null;
            importInfo = new MtdCpqImport
            {
                Id = importId,
                TimeCr = DateTime.Now,
                UserId = user,
                StatusProcess = 1,
                StatusText = "Start...",
                NoteLoad = _noteUpdate ? (sbyte)1 : (sbyte)0,
                OldToArchive = _oldToArchive ? (sbyte)1 : (sbyte)0,
                DatasheetLoad = _dataUpdate ? (sbyte)1 : (sbyte)0,
            };
            importData = new List<MtdCpqImportData>();

            await _context.MtdCpqImport.AddAsync(importInfo);
            await _context.SaveChangesAsync();

        }

        public void AddImportData(string tag, string id_number, string name, string note, string datasheet, decimal price)
        {
            string Name = name ?? "";

            MtdCpqImportData data = new MtdCpqImportData
            {
                Id = Guid.NewGuid().ToString(),
                MtdCpqImportId = importInfo.Id,
                Parent = parentNumber,
                IdNumber = id_number,
                Name = Name.Length < 254 ? Name : Name.Substring(0,254),
                Note = note ?? "",
                Datasheet = datasheet ?? "",
                Price = price,
                Required = 0,
                MasterProduct = 0,
            };

            char[] symbols = tag.ToCharArray();
            foreach (char symbol in symbols)
            {
                string s = symbol.ToString();
                if (s.Equals(importParam.TagMaster))
                {
                    parentNumber = id_number;
                    data.Parent = null;
                    data.MasterProduct = 1;
                    continue;
                }

                if (s.Equals(importParam.TagRequired))
                {
                    data.Required = 1;
                    continue;
                }

                bool isCatalog = tagsCatalog.Where(x => x.ImportTag.Contains(s)).Any();
                bool isOneOf = tagsOneOfs.Where(x => x.ImportTag.Contains(s)).Any();

                if (isCatalog) { data.TagCatalog = s; }
                if (isOneOf) { data.TagOneOf = s; }

            }

            /*Remove the double parent and all dependents*/
            var doubleParent = importData.Where(x => x.IdNumber == data.IdNumber).FirstOrDefault();
            if (data.MasterProduct == 1 && doubleParent != null)
            {
                importData.Remove(doubleParent);
                var items = new List<MtdCpqImportData>(importData).ToArray();
                foreach (var item in items)
                {
                    if (item.Parent == data.IdNumber)
                    {
                        importData.Remove(item);
                    }
                }
            }

            importData.Add(data);

        }

        public async Task SaveImportDataAsync()
        {
            try
            {
                await _context.MtdCpqImportData.AddRangeAsync(importData);
                await _context.SaveChangesAsync();

            } catch (Exception ex)
            {
                await UpdateStatusAsync($"Error SaveImportData: {ex.Message}");
            }
            
        }

        public async Task UpdateAllAsync()
        {
            
            List<MtdCpqImportData> masterList = importData.Where(x => x.Parent == null).ToList();
                    
            int masterCounter = 0;
            int masterCount = masterList.Count;
            foreach (MtdCpqImportData master in masterList)
            {
                await UpdateStatusAsync($"Start processing for {master.Name}");
                masterCounter++;
                await UpdateItemAsync(master);

                string masterId = await _context.MtdCpqProduct
                    .Where(x => x.IdNumber == master.IdNumber)
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync();

                var forRemove = await _context.MtdCpqRuleAvailable.Where(x => x.ProductIdParent == masterId).ToListAsync();
                _context.MtdCpqRuleAvailable.RemoveRange(forRemove);
                await _context.SaveChangesAsync();

                List<MtdCpqImportData> data = importData.Where(x => x.Parent == master.IdNumber).ToList();
                int count = data.Count;
                int counter = 0;
                foreach (var item in data)
                {
                    counter++;
                    await UpdateStatusAsync($"Item processing for {master.Name} product: {masterCounter} of {masterCount} items: {counter} of {count}.");
                    int sortIndex = counter + 10;
                    await UpdateItemAsync(item, masterId, sortIndex);                    
                }
            }
            
            if (!_oldToArchive) return;

            await UpdateStatusAsync($"Update archive data.");
            List<string> numbers = importData.Select(x => x.IdNumber).ToList();

            IList<MtdCpqProduct> products = await _context.MtdCpqProduct
                .Where(x => !numbers.Contains(x.IdNumber))
                .Select(x => new MtdCpqProduct
                {
                    Id = x.Id,
                    IdNumber = x.IdNumber,
                    Image = x.Image,
                    MtdCpqCatalogId = x.MtdCpqCatalogId,
                    Name = x.Name,
                    Note = x.Note,
                    Price = x.Price,
                    Som = x.Som,
                    Archive = 1
                }).ToListAsync();

            if (products.Any())
            {
                Dictionary<string, string> catalogs = await _context.MtdCpqCatalog
                    .Select(x => new { x.Id, x.ImportTag }).ToDictionaryAsync(x => x.Id, x => x.ImportTag);

                List<MtdCpqImportData> noItems = products.Select(x =>
                    new MtdCpqImportData
                    {
                        Id = Guid.NewGuid().ToString(),
                        MtdCpqImportId = importInfo.Id,
                        IdNumber = x.IdNumber,
                        Name = x.Name,
                        Price = x.Price,
                        Required = 0,
                        Parent = null,
                        Action = 3,
                        MasterProduct = x.Som,
                        TagCatalog = catalogs.Where(d => d.Key == x.MtdCpqCatalogId).Select(d => d.Value).FirstOrDefault(),
                    }).ToList();

                await _context.MtdCpqImportData.AddRangeAsync(noItems);
                _context.MtdCpqProduct.UpdateRange(products);
                List<string> productIds = products.Select(x => x.Id).ToList();
                IList<MtdCpqRuleAvailable> reomeList = await _context.MtdCpqRuleAvailable.Where(x => productIds.Contains(x.ProductIdChild) || productIds.Contains(x.ProductIdParent)).ToListAsync();
                _context.MtdCpqRuleAvailable.RemoveRange(reomeList);

                await UpdateStatusAsync($"Save changes.");
                await _context.SaveChangesAsync();
            }

        }

        public async Task UpdateStatusAsync(string text, int process = 1)
        {
            importInfo.StatusText = text;
            importInfo.StatusProcess = process;
            _context.MtdCpqImport.Update(importInfo);
            await _context.SaveChangesAsync();
        }

        private async Task UpdateItemAsync(MtdCpqImportData item, string masterId = null, int counter = 0)
        {
            string catalogId = tagsCatalog.Where(x => x.ImportTag == item.TagCatalog).Select(x => x.Id).FirstOrDefault();
            if (catalogId == null) return;
            string OneOfId = tagsOneOfs.Where(x => x.ImportTag == item.TagOneOf).Select(x => x.Id).FirstOrDefault();

            var product = _context.MtdCpqProduct
                .Where(x => x.IdNumber == item.IdNumber).FirstOrDefault();

            if (product != null)
            {
                product.Name = item.Name;
                product.Note = item.Note;
                product.Datasheet = item.Datasheet;
                product.Price = item.Price;
                product.MtdCpqCatalogId = catalogId;
                product.Som = item.MasterProduct;
                item.Action = 1;
                product.Archive = 0;
                product.Sequence = counter;               
                _context.MtdCpqProduct.Attach(product).State = EntityState.Modified;
                _context.Entry(product).Property(x => x.Note).IsModified = (_noteUpdate && item.Note != null && item.Note.Length > 0) ? true : false;
                _context.Entry(product).Property(x => x.Datasheet).IsModified = (_dataUpdate && item.Datasheet != null && item.Datasheet.Length > 0) ? true : false;
            }
            else
            {
                product = new MtdCpqProduct
                {
                    Id = Guid.NewGuid().ToString(),
                    IdNumber = item.IdNumber,
                    MtdCpqCatalogId = catalogId,
                    Name = item.Name,
                    Note = _noteUpdate ? item.Note : "",
                    Datasheet = _dataUpdate ? item.Datasheet : "",
                    Price = item.Price,
                    Som = item.MasterProduct,
                    Sequence = counter
                };

                item.Action = 2;
                await _context.MtdCpqProduct.AddAsync(product);
            }

            try
            {
                _context.MtdCpqImportData.Update(item);
                await _context.SaveChangesAsync();                                

            } catch (Exception ex)
            {
                await UpdateStatusAsync($"Error Save Item:  {ex.Message}",3);
            }
            

            if (masterId != null)
            {
                bool isExists = await _context.MtdCpqRuleAvailable
                    .Where(x => x.ProductIdParent == masterId && x.ProductIdChild == product.Id).AnyAsync();
                if (!isExists)
                {
                    MtdCpqRuleAvailable ruleAvailable = new MtdCpqRuleAvailable
                    {
                        Id = Guid.NewGuid().ToString(),
                        ProductIdParent = masterId,
                        ProductIdChild = product.Id,
                        Required = item.Required,
                        OneOfId = OneOfId
                    };

                    await _context.MtdCpqRuleAvailable.AddAsync(ruleAvailable);
                    await _context.SaveChangesAsync();
                }

            }

        }

    }


}
