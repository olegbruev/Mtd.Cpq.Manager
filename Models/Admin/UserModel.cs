using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mtd.Cpq.Manager.Models.Admin
{
    public class UserModel
    {
        [Display(Name = "ID")]
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Title name")]
        [MaxLength(128)]
        public string TitleName { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Role name" )]
        public string RoleName { get; set; }

        [Display(Name = "Group name")]
        public string GroupName { get; set; }
    }
}
