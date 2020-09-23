using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Mtd.Cpq.Manager.DataConfig;
using Mtd.Cpq.Manager.Services;

namespace Mtd.Cpq.Manager.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        private readonly IEmailSenderBlank _emailSender;     
        private readonly ConfigSettings _emailSupport;

        public ErrorModel(IEmailSenderBlank emailSender, IOptions<ConfigSettings> emailSupport)
        {
            _emailSender = emailSender;
            _emailSupport = emailSupport.Value;
        }

        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public async Task<IActionResult> OnGetAsync()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
           
            if (exceptionFeature != null)
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                BlankEmail blankEmail = new BlankEmail
                {
                    Subject = "CPQ Error",
                    Email = _emailSupport.EmailSupport,
                    Header = "Token for change password",
                    Content = new List<string> {
                        $"Request: ${RequestId}",
                        $"UserName: ${User.Identity.Name}",
                        $"Host: ${HttpContext.Request.Host.Value}",
                        $"Path: ${exceptionFeature.Path}",
                        $"Query: ${HttpContext.Request.QueryString.Value}",
                        $"Exception: ${exceptionFeature.Error.InnerException}",
                        $"Message: ${exceptionFeature.Error.Message}",
                        $"Sorce: ${exceptionFeature.Error.Source}",
                        $"StackTrace: ${exceptionFeature.Error.StackTrace}",
                    }
                };

                await _emailSender.SendEmailBlankAsync(blankEmail);
            }
            else
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
