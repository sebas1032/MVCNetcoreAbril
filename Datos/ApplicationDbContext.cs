using AplicacionWEBMVCAbril.Models;
using Microsoft.EntityFrameworkCore;

namespace AplicacionWEBMVCAbril.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        //Usar los modelos
        public DbSet<Usuario> Usuario { get; set; }

    }
}
