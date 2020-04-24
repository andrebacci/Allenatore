using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class POContextDb : POContext
    {
        // Scaffold-DbContext "Data Source=SVGENDEVSQL1\DEVTEST;Initial Catalog=purchaseOrder;User ID=novamarine;Password=novamarine" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models  -Context POContext -DataAnnotations -Force

        readonly String connString;

        public POContextDb(string connectionString) : base()
        {
            connString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //                optionsBuilder.UseSqlServer("Data Source=SVGENDEVSQL1\\DEVTEST;Initial Catalog=purchaseOrder;User ID=novamarine;Password=novamarine");

                optionsBuilder.UseSqlServer(connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<RequestQuotation>().HasQueryFilter(p => !p.FlgDeleted);
            //modelBuilder.Entity<ProtocolItems>().HasQueryFilter(p => !p.FlgDeleted);
            //modelBuilder.Entity<RequestCategory>().HasQueryFilter(p => !p.FlgDeleted);
            //modelBuilder.Entity<Request>().HasQueryFilter(p => !p.FlgDeleted);
            //modelBuilder.Entity<Protocol>().HasQueryFilter(p => !p.FlgDeleted);
            //modelBuilder.Entity<Items>().HasQueryFilter(p => !p.FlgDeleted);
        }

    }
}
