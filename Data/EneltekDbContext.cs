using Demo.Models;
using Demo.Modelsw;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Data
{
    public class EneltekDbContext:DbContext
    {
   

        public EneltekDbContext(DbContextOptions<EneltekDbContext>options) : base(options)
        {

        }

        public DbSet<Kullanici> Kullanicilar { get; set; }
    }
}
