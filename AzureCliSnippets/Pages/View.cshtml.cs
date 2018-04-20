using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureCliSnippets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AzureCliSnippets.Pages
{
    public class ViewModel : PageModel
    {
        private readonly AzureCliSnippets.Models.SnippetsContext _context;

        public ViewModel(AzureCliSnippets.Models.SnippetsContext context)
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

            // increment the view count
            Snippet.ViewCount++;
            await _context.SaveChangesAsync();

            return Page();
        }
    }
}