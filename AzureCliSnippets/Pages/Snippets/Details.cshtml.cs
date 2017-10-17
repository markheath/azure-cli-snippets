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
    public class DetailsModel : PageModel
    {
        private readonly AzureCliSnippets.Models.SnippetsContext _context;

        public DetailsModel(AzureCliSnippets.Models.SnippetsContext context)
        {
            _context = context;
        }

        public Snippet Snippet { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Snippet = await _context.Snippets.SingleOrDefaultAsync(m => m.Id == id);

            if (Snippet == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
