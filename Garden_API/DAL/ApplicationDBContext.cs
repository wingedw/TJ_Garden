using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Garden_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.Design;

namespace Garden_API.DAL
{
    public class ApplicationDBContext : DbContext
    {
         public ApplicationDBContext(DbContextOptions options) :base(options)
        {

        }  
        
        public DbSet<PlantDetails> Plants { get; set; }

    }
}
