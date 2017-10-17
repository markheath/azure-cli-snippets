using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace AzureCliSnippets.Models
{
    public class SnippetsContext : DbContext
    {
        public SnippetsContext(DbContextOptions<SnippetsContext> options)
            : base(options)
        {
        }

        public DbSet<Snippet> Snippets { get; set; }
    }
}
