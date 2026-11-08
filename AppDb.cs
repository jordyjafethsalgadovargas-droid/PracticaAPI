using Microsoft.EntityFrameworkCore;

namespace PracticaAPI
{
    //DbContext = es el puente entre tus clases de C# y la base de datos
    public class AppDb: DbContext
    {
        public AppDb(DbContextOptions<AppDb> opciones) : base(opciones){}

        // Cada DbSet es una tabla
        public DbSet<Model.Usuario> Usuarios => Set<Model.Usuario>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            //No se permiten dos cuentas con el mismo correo
            mb.Entity<Model.Usuario>().HasIndex(u => u.Correo).IsUnique();
        }
    }
}
