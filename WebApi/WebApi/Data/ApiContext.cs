using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
    public class ApiContext : DbContext
    {
           public ApiContext(DbContextOptions<ApiContext> options)
             : base(options)
           { } 
           public DbSet<Usuario> Usuarios { get; set; }
    }
}
