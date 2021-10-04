using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api
{
    public class Data : DbContext
    {
        public Data(DbContextOptions<Data> options)
            : base(options)
        {
            Database.EnsureCreated(); 
        }


        public DbSet<Car> Cars { get; set; }

        public DbSet<Provider> Providers { get; set; }
    }
}
