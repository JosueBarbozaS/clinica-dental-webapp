//Josue Barboza Segura B80937
using Microsoft.EntityFrameworkCore;


namespace AppWebClinica.Models
{
    //Muy importante que la clase Herede de la clase padre DbContext
    public class DbContextClinica : DbContext
    {
        public DbContextClinica(DbContextOptions<DbContextClinica> options) : base(options)
        {

        }

        //Propiedad DbSet que permite dar mantenimiento a las citas
        public DbSet<Cita> Citas { get; set; }
        

        //Método encargado de crear la table para el entidad en la db
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Cita>().HasData(new Cita()
            {
                Id = 1,
                Cedula = 1,
                Nombre = "Javier",
                Email = "javier@gmail.com",
                FechaHora = DateTime.Now,
                Procedimiento = "Limpieza",
                Precio = 15000,
                Impuesto = 15000 * 0.13m,
                Total = 15000 + (15000 * 0.13m),
                Adelanto = (15000 + (15000 * 0.13m)) * 0.42m
            });



        }
    }//Fin de la clase


}//Fin del namespace
