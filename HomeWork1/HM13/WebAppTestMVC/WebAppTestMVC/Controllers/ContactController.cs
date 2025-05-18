using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppTestMVC.Data;
using WebAppTestMVC.Models;

namespace WebAppTestMVC.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactDbContext _context;
        public ContactController(ContactDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var contacts = await _context.Contacts.ToListAsync();
            return View(contacts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            if(ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var contact = await _context.Contacts.FindAsync(id);
            if(contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, Contact contact)
        {
            if(id != contact.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var contactInDb = await _context.Contacts.FindAsync(contact.Id);
                    if (contactInDb == null)
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction("Index");
            }
            return View(contact);
        }
        public async Task<IActionResult> Delete(long? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var contact = await _context.Contacts.FirstOrDefaultAsync(c=>c.Id == id);
            if(contact == null) return NotFound();

            return View(contact);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteContact(long? id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
