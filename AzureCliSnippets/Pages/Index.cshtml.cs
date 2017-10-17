using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureCliSnippets.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AzureCliSnippets.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SnippetsContext _context;

        public IndexModel(SnippetsContext context)
        {
            _context = context;
        }

        public IList<Snippet> Snippets { get; set; }
        public string ErrorMessage { get; set;  }
        public async Task OnGetAsync()
        {
            try
            {
                Snippets = await _context.Snippets.ToListAsync();
            }
            catch (Exception ex)
            {
                Snippets = new List<Snippet>();
                ErrorMessage = "Could not retrieve snippets";
            }
        }
    }
}
