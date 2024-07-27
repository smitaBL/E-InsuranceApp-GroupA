using Microsoft.EntityFrameworkCore;
using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Context
{
    public class EInsuranceDbContext : DbContext
    {
        public EInsuranceDbContext(DbContextOptions<EInsuranceDbContext> options) : base(options) { }

        public DbSet<AdminEntity> Admins { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }    
        public DbSet<CustomerEntity> Customers { get; set; }    
        public DbSet<InsuranceAgentEntity> InsuranceAgents { get; set;}
        public DbSet<InsurancePlanEntity> InsurancePlans { get; set; }
        public DbSet<SchemeEntity> Schemes { get; set; }
        public DbSet<PolicyEntity> Policies { get; set; }
        public DbSet<PaymentEntity> Payments { get; set; }
        public DbSet<CommissionEntity> Commissions { get; set; }
        public DbSet<EmployeeSchemeEntity> EmployeeSchemes { get; set; }
        public DbSet<SchemeWithInsurancePlanML> SchemeWithInsurancePlanML { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AdminEntity>().HasIndex(a => a.Email).IsUnique();
            modelBuilder.Entity<EmployeeEntity>().HasIndex(e => e.Email).IsUnique();
            modelBuilder.Entity<CustomerEntity>().HasIndex(c => c.Email).IsUnique();
            modelBuilder.Entity<InsuranceAgentEntity>().HasIndex(i => i.Email).IsUnique();

            modelBuilder.Entity<AdminEntity>().HasIndex(a => a.Username).IsUnique();
            modelBuilder.Entity<EmployeeEntity>().HasIndex(e => e.Username).IsUnique();
            modelBuilder.Entity<CustomerEntity>().HasIndex(c => c.Username).IsUnique();
            modelBuilder.Entity<InsuranceAgentEntity>().HasIndex(i => i.Username).IsUnique();

            modelBuilder.Entity<CustomerEntity>()
                .HasOne(a => a.InsuranceAgent)
                .WithMany(b => b.Customers)
                .HasForeignKey(c => c.AgentID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PaymentEntity>()
               .HasOne(p => p.Customer)
               .WithMany(c => c.Payments)
               .HasForeignKey(p => p.CustomerID)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PaymentEntity>()
                .HasOne(p => p.Policy)
                .WithMany(po => po.Payments)
                .HasForeignKey(p => p.PolicyID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EmployeeSchemeEntity>()
               .HasOne(es => es.Employee)
               .WithMany(e => e.EmployeeSchemes)
               .HasForeignKey(es => es.EmployeeID)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EmployeeSchemeEntity>()
                .HasOne(es => es.Scheme)
                .WithMany(s => s.EmployeeSchemes)
                .HasForeignKey(es => es.SchemeID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SchemeWithInsurancePlanML>().HasNoKey();
        }
    }
}
