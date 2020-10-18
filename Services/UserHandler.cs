using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mtd.Cpq.Manager.DataConfig;
using Mtd.OrderMaker.Server.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Mtd.OrderMaker.Server.Data;
using Mtd.Cpq.Manager.Areas.Identity.Data;

namespace Mtd.Cpq.Manager.Services
{
    public class UserHandler : UserManager<WebAppUser>
    {
        /*Dont change sequence! It's int code in database!*/
        public enum ActionType
        {
            CreateTitles, SetTitlesOwner, EditTitles, RemoveTitles, RemoveTitlesOwner,
            SetQuoteOwner, CreateQuote, RemoveQuote, EditQuote, RemoveQuoteOwner, ConfigChange,
            ConfigDelete, ConfigInclude, ConfigRule, EditPrice, EditTotal,
            ProductCreate, ProductEdit, ProductAnchorLink, ProductAnchorNotice, ProductDelete, ProductRelationAvailables, ProductRelationRequired, ProductRelationOneOf
        }


        public enum CpqPolicyView { ViewAll, ViewGroup, ViewOwn }
        public enum CpqPolicyEdit { EditAll, EditGroup, EditOwn }

        public class UserParams
        {
            public bool PrintGrossPrice;
            public CpqPolicyView CpqPolicyView;
            public CpqPolicyEdit CpqPolicyEdit;
            public IList<string> GroupTitleIds;
            public IList<string> OwnTitleIds;
        }

        public static string ClaimNameGroup => "claimGroup";

        private readonly IdentityDbContext identity;
        private readonly ILogger<UserManager<WebAppUser>> logger;

        public UserHandler(
            IdentityDbContext identity,
            IUserStore<WebAppUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<WebAppUser> passwordHasher,
            IEnumerable<IUserValidator<WebAppUser>> userValidators,
            IEnumerable<IPasswordValidator<WebAppUser>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
            IServiceProvider services, ILogger<UserManager<WebAppUser>> logger) :
            base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            this.identity = identity;
            this.logger = logger;
        }

        public async Task<List<string>> MenuUserAsync(ClaimsPrincipal user)
        {
            List<string> menu = new List<string>();
            WebAppUser appUser = await GetUserAsync(user);
            IList<string> roles = await this.GetRolesAsync(appUser);
            if (roles.Contains(SystemRoles.Admin))
            {
                menu.Add("Sales");
                menu.Add("Goods");
                menu.Add("Supervision");
                menu.Add("Admin");
            }

            if (roles.Contains(SystemRoles.GoodsManager))
            {
                menu.Add("Sales");
                menu.Add("Supervision");
                menu.Add("Goods");
            }

            if (roles.Contains(SystemRoles.Supervisor))
            {
                menu.Add("Sales");
                menu.Add("Supervision");
            }

            if (roles.Contains(SystemRoles.SalesManager))
            {
                menu.Add("Sales");
            }

            return menu;
        }

        public async Task<bool> SetTitlesOwnerAsync(string guid, string newUserId, ClaimsPrincipal user)
        {
            WebAppUser appUser = await FindByIdAsync(newUserId);

            if (appUser == null) { return false; }
            MtdCpqTitlesOwner owner = await identity.MtdCpqTitlesOwners.FindAsync(guid);
            if (owner == null)
            {
                owner = new MtdCpqTitlesOwner
                {
                    Id = guid,
                    UserId = appUser.Id,
                    UserName = appUser.Title
                };
                await identity.MtdCpqTitlesOwners.AddAsync(owner);
            }
            else
            {
                owner.UserId = appUser.Id;
                owner.UserName = appUser.Title;
                identity.MtdCpqTitlesOwners.Update(owner);
            }

            try
            {
                await identity.SaveChangesAsync();
                await AddActionLogAsync(ActionType.SetTitlesOwner, user, owner.Id);
            }
            catch
            {
                return false;
            }


            return true;
        }

        public async Task<bool> SetQuoteOwnerAsync(string guid, string newUserId, ClaimsPrincipal user)
        {
            WebAppUser appUser = await FindByIdAsync(newUserId);

            if (appUser == null) { return false; }
            MtdCpqProposalOwner owner = await identity.MtdCpqProposalOwners.FindAsync(guid);
            if (owner == null)
            {
                owner = new MtdCpqProposalOwner
                {
                    Id = guid,
                    UserId = appUser.Id,
                    UserName = appUser.Title
                };
                await identity.MtdCpqProposalOwners.AddAsync(owner);
            }
            else
            {
                owner.UserId = appUser.Id;
                owner.UserName = appUser.Title;
                identity.MtdCpqProposalOwners.Update(owner);
            }

            try
            {
                await identity.SaveChangesAsync();
                await AddActionLogAsync(ActionType.SetTitlesOwner, user, owner.Id);
            }
            catch
            {
                return false;
            }


            return true;
        }

        public async Task<bool> RemoveTitlesOwnerAsync(string guid, ClaimsPrincipal user)
        {
            WebAppUser appUser = await GetUserAsync(user);

            if (appUser == null) { return false; }
            MtdCpqTitlesOwner owner = await identity.MtdCpqTitlesOwners.FindAsync(guid);
            if (owner == null) { return false; }

            identity.MtdCpqTitlesOwners.Remove(owner);

            try
            {
                await identity.SaveChangesAsync();
                await AddActionLogAsync(ActionType.RemoveTitlesOwner, user, owner.Id);
            }
            catch
            {
                return false;
            }


            return true;
        }

        public async Task<bool> RemoveQuotesOwnerAsync(string guid, ClaimsPrincipal user)
        {
            WebAppUser appUser = await GetUserAsync(user);

            if (appUser == null) { return false; }
            MtdCpqProposalOwner owner = await identity.MtdCpqProposalOwners.FindAsync(guid);
            if (owner == null) { return false; }

            identity.MtdCpqProposalOwners.Remove(owner);

            try
            {
                await identity.SaveChangesAsync();
                await AddActionLogAsync(ActionType.RemoveQuoteOwner, user, owner.Id);
            }
            catch
            {
                return false;
            }


            return true;
        }

        public async Task<bool> AddActionLogAsync(ActionType actionType, ClaimsPrincipal user, string documentId = "no document")
        {
            string userId = "error user id";
            string userName = "error user name";
            WebAppUser appUser = await GetUserAsync(user);
            if (appUser != null) { userId = appUser.Id; userName = appUser.Title; }

            try
            {
                await identity.MtdCpqLogActions.AddAsync(new MtdCpqLogAction
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    ActionTime = DateTime.UtcNow,
                    ActionType = (int)actionType,
                    DocumentId = documentId,
                    UserName = userName
                });

                await identity.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                logger.LogCritical($"ERROR! AddActionLogAsync: {ex.Message}");
                return false;
            }


            return true;

        }

        public async Task<IList<string>> GetTitlesIdsForUsersAsync(IList<string> userIds)
        {
            IList<string> titlesIds = await identity.MtdCpqTitlesOwners.Where(x => userIds.Contains(x.UserId)).Select(x => x.Id).ToListAsync();
            return titlesIds;
        }

        public async Task<IList<string>> GetQuotesIdsForUsersAsync(IList<string> userIds)
        {
            IList<string> quotesIds = await identity.MtdCpqProposalOwners.Where(x => userIds.Contains(x.UserId)).Select(x => x.Id).ToListAsync();
            return quotesIds;
        }

        private async Task<string> GetUserLongNameAsync(string userId)
        {
            WebAppUser user = await FindByIdAsync(userId);
            return $"{user.Title} {user.TitleGroup}";
        }

        public async Task<Dictionary<string, WebAppUser>> GetOwnersTitlesAsync(IList<string> titlesIds)
        {
            Dictionary<string, WebAppUser> dictonary = new Dictionary<string, WebAppUser>();
            IList<MtdCpqTitlesOwner> list = await identity.MtdCpqTitlesOwners.Where(x => titlesIds.Contains(x.Id)).ToListAsync();
            IList<string> userIds = list.Select(x => x.UserId).ToList();
            IList<WebAppUser> users = await Users.Where(x => userIds.Contains(x.Id)).ToListAsync();
            foreach (var item in list)
            {
                WebAppUser appUser = users.FirstOrDefault(x => x.Id == item.UserId);
                dictonary.Add(item.Id, appUser);
            }

            return dictonary;
        }
        public async Task<Dictionary<string, WebAppUser>> GetOwnersQuotesAsync(IList<string> quotesIds)
        {
            Dictionary<string, WebAppUser> dictonary = new Dictionary<string, WebAppUser>();
            IList<MtdCpqProposalOwner> list = await identity.MtdCpqProposalOwners.Where(x => quotesIds.Contains(x.Id)).ToListAsync();
            IList<string> userIds = list.Select(x => x.UserId).ToList();
            IList<WebAppUser> users = await Users.Where(x => userIds.Contains(x.Id)).ToListAsync();
            foreach (var item in list)
            {
                WebAppUser appUser = users.FirstOrDefault(x => x.Id == item.UserId);
                dictonary.Add(item.Id, appUser);
            }

            return dictonary;
        }

        public async Task<string> GetTitlesOwnerNameAsync(string guid)
        {
            var owner = await identity.MtdCpqTitlesOwners.FindAsync(guid);
            if (owner == null) { return "No owner"; }
            string longname = await GetUserLongNameAsync(owner.UserId);
            return longname;
        }
        public async Task<string> GetQuoteOwnerNameAsync(string guid)
        {
            var owner = await identity.MtdCpqProposalOwners.FindAsync(guid);
            if (owner == null) { return "No owner"; }
            string longname = await GetUserLongNameAsync(owner.UserId);
            return longname;
        }

        private async Task<IList<WebAppUser>> GetUsersInGroupsAsync(ClaimsPrincipal user)
        {

            List<WebAppUser> webAppUsers = new List<WebAppUser>();
            IList<Claim> groups = user.Claims.Where(c => c.Type == "group").ToList();

            foreach (var claim in groups)
            {
                IList<WebAppUser> users = await GetUsersForClaimAsync(claim);
                if (users != null)
                {
                    var temp = users.Where(x => !webAppUsers.Select(w => w.Id).Contains(x.Id)).ToList();
                    if (temp != null)
                    {
                        webAppUsers.AddRange(temp);
                    }
                }
            }

            return webAppUsers;
        }

        public async Task<UserParams> GetTitlesForUserAsync(ClaimsPrincipal user)
        {
            WebAppUser appUser = await GetUserAsync(user);
            UserParams userParams = GetCpqPolicy(user);
            IList<WebAppUser> users = await GetUsersInGroupsAsync(user);
            IList<string> userIds = users.Select(x => x.Id).ToList();
            userParams.GroupTitleIds = new List<string>();
            
            IList<string> allTitleIds = await identity.MtdCpqTitlesOwners.Where(x => userIds.Contains(x.UserId) && x.UserId != appUser.Id).Select(x => x.Id).ToListAsync();
            if (allTitleIds != null)
            {
                userParams.GroupTitleIds = allTitleIds;
            }

            userParams.OwnTitleIds = new List<string>();
            IList<string> ownTitleIds = await identity.MtdCpqTitlesOwners.Where(x => x.UserId == appUser.Id).Select(x => x.Id).ToListAsync();
            if (ownTitleIds != null)
            {
                userParams.OwnTitleIds = ownTitleIds;
            }

            //if (userParams.CpqPolicyView.Equals(CpqPolicyView.ViewGroup) && !onlyOwn)
            //{
            //    IList<WebAppUser> users = await GetUsersInGroupsAsync(user);
            //    IList<string> userIds = users.Select(x => x.Id).ToList();
            //    userParams.AllTitleIds = await identity.MtdCpqTitlesOwners.Where(x => userIds.Contains(x.UserId)).Select(x => x.Id).ToListAsync();
            //}

            //if (userParams.CpqPolicyView.Equals(CpqPolicyView.ViewOwn) || onlyOwn)
            //{
            //    WebAppUser appUser = await GetUserAsync(user);
            //    userParams.AllTitleIds = await identity.MtdCpqTitlesOwners.Where(x => x.UserId == appUser.Id).Select(x => x.Id).ToListAsync();
            //}

            return userParams;
        }

        public async Task<UserParams> GetQuotesForUserAsync(ClaimsPrincipal user, bool onlyOwn = false)
        {
            UserParams userParams = GetCpqPolicy(user);

            if (userParams.CpqPolicyView.Equals(CpqPolicyView.ViewGroup) && !onlyOwn)
            {
                IList<WebAppUser> users = await GetUsersInGroupsAsync(user);
                IList<string> userIds = users.Select(x => x.Id).ToList();
                userParams.GroupTitleIds = await identity.MtdCpqProposalOwners.Where(x => userIds.Contains(x.UserId)).Select(x => x.Id).ToListAsync();
            }

            if (userParams.CpqPolicyView.Equals(CpqPolicyView.ViewOwn) || onlyOwn)
            {
                WebAppUser appUser = await GetUserAsync(user);
                userParams.GroupTitleIds = await identity.MtdCpqProposalOwners.Where(x => x.UserId == appUser.Id).Select(x => x.Id).ToListAsync();
            }

            return userParams;
        }

        public async Task<bool> CheckQuoteAccessAsync(string guid, ClaimsPrincipal user)
        {
            UserParams userParams = GetCpqPolicy(user);
            if (userParams.CpqPolicyView == CpqPolicyView.ViewAll) { return true; }

            if (userParams.CpqPolicyView.Equals(CpqPolicyView.ViewGroup))
            {
                IList<WebAppUser> users = await GetUsersInGroupsAsync(user);
                IList<string> userIds = users.Select(x => x.Id).ToList();
                return await identity.MtdCpqProposalOwners.Where(x => userIds.Contains(x.UserId) && x.Id == guid).AnyAsync();
            }

            if (userParams.CpqPolicyView.Equals(CpqPolicyView.ViewOwn))
            {
                WebAppUser appUser = await GetUserAsync(user);
                return await identity.MtdCpqProposalOwners.Where(x => x.UserId == appUser.Id && x.Id == guid).AnyAsync();
            }

            return false;
        }
        public async Task<bool> CheckQuoteEditAsync(string guid, ClaimsPrincipal user)
        {
            UserParams userParams = GetCpqPolicy(user);
            if (userParams.CpqPolicyEdit == CpqPolicyEdit.EditAll) { return true; }

            if (userParams.CpqPolicyEdit.Equals(CpqPolicyEdit.EditGroup))
            {
                IList<WebAppUser> users = await GetUsersInGroupsAsync(user);
                IList<string> userIds = users.Select(x => x.Id).ToList();
                return await identity.MtdCpqProposalOwners.Where(x => userIds.Contains(x.UserId) && x.Id == guid).AnyAsync();
            }

            if (userParams.CpqPolicyEdit.Equals(CpqPolicyEdit.EditOwn))
            {
                WebAppUser appUser = await GetUserAsync(user);
                return await identity.MtdCpqProposalOwners.Where(x => x.UserId == appUser.Id && x.Id == guid).AnyAsync();
            }

            return false;
        }

        public async Task<bool> CheckTitlesAccessAsync(string guid, ClaimsPrincipal user)
        {
            UserParams userParams = GetCpqPolicy(user);
            if (userParams.CpqPolicyView == CpqPolicyView.ViewAll) { return true; }

            if (userParams.CpqPolicyView.Equals(CpqPolicyView.ViewGroup))
            {
                IList<WebAppUser> users = await GetUsersInGroupsAsync(user);
                IList<string> userIds = users.Select(x => x.Id).ToList();
                return await identity.MtdCpqTitlesOwners.Where(x => userIds.Contains(x.UserId) && x.Id == guid).AnyAsync();
            }

            if (userParams.CpqPolicyView.Equals(CpqPolicyView.ViewOwn))
            {
                WebAppUser appUser = await GetUserAsync(user);
                return await identity.MtdCpqTitlesOwners.Where(x => x.UserId == appUser.Id && x.Id == guid).AnyAsync();
            }

            return false;
        }
        public async Task<bool> CheckTitlesEditAsync(string guid, ClaimsPrincipal user)
        {
            UserParams userParams = GetCpqPolicy(user);
            if (userParams.CpqPolicyEdit == CpqPolicyEdit.EditAll) { return true; }

            if (userParams.CpqPolicyEdit.Equals(CpqPolicyEdit.EditGroup))
            {
                IList<WebAppUser> users = await GetUsersInGroupsAsync(user);
                IList<string> userIds = users.Select(x => x.Id).ToList();
                return await identity.MtdCpqTitlesOwners.Where(x => userIds.Contains(x.UserId) && x.Id == guid).AnyAsync();
            }

            if (userParams.CpqPolicyEdit.Equals(CpqPolicyEdit.EditOwn))
            {
                WebAppUser appUser = await GetUserAsync(user);
                return await identity.MtdCpqTitlesOwners.Where(x => x.UserId == appUser.Id && x.Id == guid).AnyAsync();
            }

            return false;
        }

        public UserParams GetCpqPolicy(ClaimsPrincipal user)
        {
            UserParams userParams = new UserParams { CpqPolicyEdit = CpqPolicyEdit.EditOwn, CpqPolicyView = CpqPolicyView.ViewOwn, PrintGrossPrice = false };
            if (user == null) { return userParams; }
            Claim claim = user.Claims.Where(x => x.Type == "cpq-policy").FirstOrDefault();
            if (claim == null) { return userParams; }
            if (claim.Value.Contains("view-all")) { userParams.CpqPolicyView = CpqPolicyView.ViewAll; }
            if (claim.Value.Contains("view-group")) { userParams.CpqPolicyView = CpqPolicyView.ViewGroup; }
            if (claim.Value.Contains("edit-all")) { userParams.CpqPolicyEdit = CpqPolicyEdit.EditAll; }
            if (claim.Value.Contains("edit-group")) { userParams.CpqPolicyEdit = CpqPolicyEdit.EditGroup; }
            if (claim.Value.Contains("print-gross-price")) { userParams.PrintGrossPrice = true; }

            return userParams;
        }

    }
}
