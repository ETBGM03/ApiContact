using ApiContact.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiContact.Data
{
    public class ContactListDb: DbContext
    {
        public ContactListDb(DbContextOptions<ContactListDb> options): base(options) { }

        public DbSet<ContactList> Contacts => Set<ContactList>();

    }
}
