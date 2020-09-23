using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mtd.Cpq.Manager.Models
{ 
    public class LanguageItem
    {
        public string Culture { get; }
        public string Name { get; }

        public LanguageItem(string culture, string name)
        {
            Culture = culture;
            Name = name;
        }
    }
    public class Language
    {
        public List<LanguageItem> Items { get; }

        public Language()
        {
            Items = new List<LanguageItem>
        {
            new LanguageItem ("en-US", "English"),
            new LanguageItem ("ru-RU", "Русский")
        };
        }

    }
}
