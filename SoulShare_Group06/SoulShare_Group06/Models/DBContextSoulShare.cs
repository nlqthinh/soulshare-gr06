using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SoulShare_Group06.Models
{
    public partial class DBContextSoulShare : DbContext
    {
        public DBContextSoulShare()
            : base("name=DBContextSoulShareConnectionString")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.customer_name)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.customer_email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.customer_address)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.account_password)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.account_phone)
                .IsUnicode(false);
        }
    }
}
