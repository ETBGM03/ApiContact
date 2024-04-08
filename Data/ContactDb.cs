using ApiContact.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiContact.Data
{
    public class ContactDb: DbContext
    {
        public ContactDb(DbContextOptions<ContactDb> options): base(options) { }
        public DbSet<Contact> Contacts => Set<Contact>();
        public DbSet<ExtraFields> ExtraFields { get; set; }
        public DbSet<ContactType> ContactType { get; set; }
        
    }
}
