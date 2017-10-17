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
        public async Task OnGetAsync()
        {
            Snippets = await _context.Snippets.Take(10).ToListAsync();
        }
    }
}
