using EMP.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EMP.Entities
{
    public partial class EmployeeDbContext : DbContext
    {
        public virtual DbSet<Employee> Employees { get; set; }

        public EmployeeDbContext()
        {
            Database.SetCommandTimeout(300);
        }

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=MyDatabase;trusted_connection=true;");
            //base.OnConfiguring(optionsBuilder); 
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");
            
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees");

                entity.Property(e => e.Employeeid)
                    .ValueGeneratedNever()
                    .HasColumnName("employeeid");

                entity.Property(e => e.Employeename)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("employeename");

                entity.Property(e => e.Employeesalary).HasColumnName("employeesalary");

                //entity.Property(e => e.Existenceendutc).HasColumnName("existenceendutc");

                //entity.Property(e => e.Existencestartutc).HasColumnName("existencestartutc");
            });

            //foreach (var entity in modelBuilder.Model.GetEntityTypes())
            //{
            //    foreach (var prop in entity.GetProperties())
            //    {
            //        if (prop.Name == "existencestartutc" || prop.Name == "existenceendutc")
            //        {
            //            prop.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAddOrUpdate;
            //        }
            //    }
            //}

            OnModelCreatingPartial(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
