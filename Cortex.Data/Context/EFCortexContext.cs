using Cortex.Domain.Entity;
using System;
using System.Data.Entity;

namespace Cortex.Data.Context
{
    public class EFCortexContext : DbContext
    {
        public EFCortexContext()
            : base("DataContext")
        {
        }
        public DbSet<Personal> Personals { get; set; }

        //public override int SaveChanges()
        //{
        //    var result = 0;
        //    try
        //    {
        //        result =SaveChanges();
        //    }
        //    catch (Exception ex) 
        //    {

        //        throw new DatabaseException("DataBaseException",ex);
        //    }
        //    return base.SaveChanges();
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}