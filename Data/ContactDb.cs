using ApiContact.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiContact.Data
{
    public class ContactDb: DbContext
    {
        public ContactDb(DbContextOptions<ContactDb> options): base(options) { }

        public DbSet<Contact> Contacts => Set<Contact>();
        public DbSet<ContactType> ContactsTypes => Set<ContactType>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .HasMany(contact => contact.ContactTypes)
                .WithMany(contactType => contactType.Contacts)
                .UsingEntity(j => j.ToTable("ContactContactType"));
        }

    }
}
