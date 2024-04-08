using System.Text.Json;
using System.Text.Json.Serialization;
using ApiContact.Data;
using ApiContact.DataTypes;
using ApiContact.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExtraFields = ApiContact.Models.ExtraFields;

namespace ApiContact.Controllers
{
    [ApiController]
    [Route("api/contact")]
    public class ContactController : ControllerBase
    {
        private readonly ContactDb _db;

        public ContactController(ContactDb db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            var contactList = await _db.Contacts
                .Include(c => c.ExtraFields)
                .Include(c => c.ContactType)
                .ToListAsync();

            if (contactList.Count == 0)
            {
                return NotFound("Your contact list is empty");
            }

            return Ok(contactList);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Contact>> GetContactById(int id)
        {
            
            
            var contact = await _db.Contacts
                .Include(c => c.ExtraFields)
                .SingleOrDefaultAsync(contact => contact.Id == id);

            if (contact == null)
            {
                return NotFound("Contact not found with this Id");
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var serializedContact = JsonSerializer.Serialize(contact, options);

            return Ok(serializedContact);
        }

        [HttpPost]
        public async Task<ActionResult<InputContact>> CreateContact(InputContact newContact)
        {
            var contact = new Contact();
            var contactType = await _db.ContactType.FirstOrDefaultAsync(ct => ct.Type == newContact.ContactType);
            contact.Name = newContact.Name;
            contact.Phone = newContact.Phone;
            contact.Comments = newContact.Comments;
            if (contactType is { Id: 0 })
            {
                contact.ContactType = new ContactType { Type = "Person", Id = 1 };
            }
            
            if (contactType is { Type: not null })
            {
                contact.ContactTypeId = contactType.Id;

            }
            _db.Contacts.Add(contact);
            await _db.SaveChangesAsync();

            foreach (var extraField in newContact.ExtraFields)
            {
                var field = new ExtraFields();

                field.Field = extraField.Field;
                field.Value = extraField.Value;
                field.ContactId = contact.Id;

                _db.ExtraFields.Add(field);
            }

            await _db.SaveChangesAsync();


            return Created($"/contact/{contact.Id}", newContact);
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<Contact>> UpdateContact(int id, InputPatchContact updatedContact)
        {
            try
            {
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteContact(int id)
        {
            var contact = await _db.Contacts.FindAsync(id);
            var extraFields = await _db.ExtraFields.Where(ef => ef.ContactId == id).ToListAsync();

            if (contact == null)
            {
                return NotFound("Contact not found with this Id");
            }

            foreach (var extraField in extraFields)
            {
                _db.ExtraFields.Remove(extraField);
            }

            await _db.SaveChangesAsync();

            _db.Contacts.Remove(contact);
            await _db.SaveChangesAsync();

            return Ok("Contact deleted successfully");
        }

    }
}