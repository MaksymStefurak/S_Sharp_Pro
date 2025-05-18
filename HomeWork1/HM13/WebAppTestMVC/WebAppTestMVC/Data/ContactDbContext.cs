using Microsoft.EntityFrameworkCore;
using WebAppTestMVC.Models;

namespace WebAppTestMVC.Data
{
    public class ContactDbContext: DbContext
    {

        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
