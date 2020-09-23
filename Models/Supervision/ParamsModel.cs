using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mtd.Cpq.Manager.Models.Supervision
{
    public class ParamsModel
    {
        [Key]
        [Display(Name = "ID")]
        public string Id { get; set; }

        [Display(Name = "Name")]
        [MaxLength(128)]
        public string Name { get; set; }

        [Display(Name = "Note")]
        [MaxLength(255)]
        public string Note { get; set; }
       
        [Required]
        [Display(Name = "Prefix")]
        public string Prefix { get; set; }
               
        [Display(Name = "Logo")]
        public byte[] Logo { get; set; }

        public SelectList CurencyList { get; set; }
        public SelectList LanguageList { get; set; }

    }
}
