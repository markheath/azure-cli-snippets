using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AzureCliSnippets.Models;

namespace AzureCliSnippets.Pages.Snippets
{
    public class IndexModel : PageModel
    {
        private readonly AzureCliSnippets.Models.SnippetsContext _context;

        public IndexModel(AzureCliSnippets.Models.SnippetsContext context)
        {
            _context = context;
        }

        public IList<Snippet> Snippet { get;set; }

        public async Task OnGetAsync()
        {
            Snippet = await _context.Snippets.ToListAsync();
        }
    }
}
