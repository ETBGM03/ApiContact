using ApiContact.Data;
using ApiContact.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiContact.Controllers
{
    [ApiController]
    [Route("api/contact")]
    public class ContactController : ControllerBase
    {

        private readonly ContactDb _db;
        public ContactController (ContactDb db)
        {
            _db = db;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            var contactList = await _db.Contacts.ToListAsync();

            if (contactList.Count == 0)
            {
                return NotFound("Your contact list is empty");
            }

            return Ok(contactList);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Contact>> GetContactById(int id)
        {
            var contact = await _db.Contacts.SingleOrDefaultAsync(contact => contact.Id == id);

            if (contact == null)
            {
                return NotFound("Contact not found with this Id");
            }

            return Ok(contact);

        }


        [HttpPost]
        public async Task<ActionResult<Contact>> CreateContact(Contact newContact)
        {
            _db.Contacts.Add(newContact);
            await _db.SaveChangesAsync();
            return Created($"/contact/{newContact.Id}", newContact);
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<Contact>> UpdateContact(int id, Contact updatedContact)
        {
            if (id != updatedContact.Id)
            {
                return BadRequest("The id does not match");
            }

            var contact = await _db.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound("Contact not found with this Id");
            }

            contact.Name = updatedContact.Name;
            contact.Phone = updatedContact.Phone;
            contact.Comments = updatedContact.Comments;

            await _db.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteContact(int id)
        {
            var contact = await _db.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound("Contact not found with this Id");
            }

            _db.Contacts.Remove(contact);
            await _db.SaveChangesAsync();

            return Ok("Contact deleted successfully");
        }

    }
}
