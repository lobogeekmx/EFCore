using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EFCorePeliculas.Entidades;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculas
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {

        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>().HaveColumnType("date");
            configurationBuilder.Properties<string>().HaveMaxLength(400);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //Seteo de PK
            //modelBuilder.Entity<Genero>().HasKey(prop => prop.Id); 

            //Longitud maxima de un string
            // modelBuilder.Entity<Genero>().Property(prop => prop.Nombre)
            //     .HasColumnName("Generos")
            //     .HasMaxLength(150);
            //     .IsRequired();

            //modelBuilder.Entity<Genero>().ToTable(name:"Generos", schema:"dbo");

            // modelBuilder.Entity<Pelicula>().Property(prop => prop.PosterUrl)
            //     .IsUnicode(false);

            // Setear valor por defecto
            // modelBuilder.Entity<SalaDeCine>().Property(prop => prop.TipoSalaDeCine)
            //     .HasDefaultValue(TipoSalaDeCine.DosDimensiones);

            

            // LLave primaria compuesta
            // modelBuilder.Entity<PeliculaActor>().HasKey(prop => 
            //     new {prop.PeliculaId, prop.ActorId});
        }

        public DbSet<Genero> Generos{get;set;}
        public DbSet<Actor> Actores{get;set;}
        public DbSet<Cine> Cines{get;set;}
        public DbSet<Pelicula> Peliculas{get;set;}
        public DbSet<CineOferta> CinesOfertas{get;set;}
        public DbSet<SalaDeCine> SalasDeCine{get;set;}
        public DbSet<PeliculaActor> PeliculasActores{get;set;}
    }
}