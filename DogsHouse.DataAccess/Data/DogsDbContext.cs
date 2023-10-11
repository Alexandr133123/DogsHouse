using DogsHouse.Application.Common.Interfaces;
using DogsHouse.DataAccess.Data.Configurations;
using DogsHouseService.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouse.DataAccess.Data
{
    public class DogsDbContext : DbContext, IDogsDbContext
    {
        public DbSet<Dog> Dogs { get; set; }

        public DogsDbContext(DbContextOptions<DogsDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DogConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
