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
    public class MigrateModel : PageModel
    {
        private readonly SnippetsContext _context;

        public string Message { get; set; }
        public string ErrorMessage { get; set; }

        public MigrateModel(SnippetsContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            try
            {
                _context.Database.Migrate();
                Message = "Migration Success";
            }
            catch (Exception e)
            {
                ErrorMessage = $"Failed to migrate {e.Message}";
            }
        }
    }
}