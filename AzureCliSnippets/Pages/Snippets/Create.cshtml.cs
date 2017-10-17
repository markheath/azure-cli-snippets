using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AzureCliSnippets.Models;

namespace AzureCliSnippets.Pages.Snippets
{
    public class CreateModel : PageModel
    {
        private readonly AzureCliSnippets.Models.SnippetsContext _context;

        public CreateModel(AzureCliSnippets.Models.SnippetsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Snippet Snippet { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Snippets.Add(Snippet);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}