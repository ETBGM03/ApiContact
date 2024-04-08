using ApiContact.Data;
using ApiContact.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiContact.Controllers
{
    [ApiController]
    [Route("api/contact-types")]
    public class ContactTypeController : ControllerBase
    {

        private readonly ContactDb _db;
        public ContactTypeController (ContactDb db)
        {
            _db = db;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            var contactList = await _db.ContactType.ToListAsync();

            if (contactList.Count == 0)
            {
                return NotFound("Your contact types list is empty");
            }

            return Ok(contactList);
        }
    }
}
