using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mtd.Cpq.Manager.DataConfig
{
    public class SystemRole
    {
        public string Name { get; set; }
        public string Title { get; set; }
    }

    public static class SystemRoles
    {
        public const string Admin = "cpq-admin";
        public const string GoodsManager = "cpq-goodsmanager";
        public const string Supervisor = "cpq-supervisor";
        public const string SalesManager = "cpq-salesmanager";
        public const string Guest = "cpq-guest";

        public static string GetTitleName(string role)
        {
            string result;

            switch (role)
            {
                case Admin: { result = "Administrator"; break; }
                case GoodsManager: { result = "Goods manager"; break; }
                case Supervisor: { result = "Supervisor"; break; }
                case SalesManager: { result = "Sales Manager"; break; }
                default: { result = "Guest"; break; }
            }

            return result;
        }

        public static List<SystemRole> GetRoles()
        {
            List<SystemRole> systemRoles = new List<SystemRole>() {
                new SystemRole{Name = Admin, Title = GetTitleName(Admin)},
                new SystemRole{Name = GoodsManager, Title = GetTitleName(GoodsManager)},
                new SystemRole{Name = Supervisor,Title = GetTitleName(Supervisor)},
                new SystemRole{Name = SalesManager, Title = GetTitleName(SalesManager)},
                new SystemRole{Name = Guest,Title = GetTitleName(Guest)}
            };

            return systemRoles;
        }

    }
}
