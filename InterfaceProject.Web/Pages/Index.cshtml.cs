using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterfaceProject.Web.Pages
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// http://localhost/Index
        /// </summary>
        /// <returns>微信nonce原字符串或者首页</returns>
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
