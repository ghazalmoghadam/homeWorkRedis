using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Domain.DomainModel.Models;
using PhoneBook.DomainModel;
using System;

namespace PhoneBook.Infrastructure.DataAccess.EF
{
    public class PhoneBookDbContext: DbContext
    {
        public PhoneBookDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().Property(p=>p.PhoneNumber).HasMaxLength(11).IsFixedLength();
            base.OnModelCreating(modelBuilder);
        }
    }
}
