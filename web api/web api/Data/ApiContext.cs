using Microsoft.EntityFrameworkCore;
using web_api.Models;

namespace web_api.Data
{
    public class ApiContext : DbContext
    {
           public ApiContext(DbContextOptions<ApiContext> options)
             : base(options)
           { } 

           public DbSet<Usuario> Usuarios { get; set; }
    }
}
