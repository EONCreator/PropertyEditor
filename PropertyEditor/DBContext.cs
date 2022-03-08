using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PropertyEditor.Models;

namespace PropertyEditor.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Object> Objects { get; set; }

        #region Properties
        
        public DbSet<IntegerProperty> IntegerProperties { get; set; }
        public DbSet<IntegerValue> IntegerValues { get; set; }

        public DbSet<StringProperty> StringProperties { get; set; }
        public DbSet<StringValue> StringValues { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
