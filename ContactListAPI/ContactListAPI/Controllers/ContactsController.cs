using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContactListAPI.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class ContactsController: ControllerBase
    {
        public static readonly List<Contact> contacts = new List<Contact> {
            new Contact { Id = 0, FirstName = "Eleasha", LastName = "Larsen", Email = "e.larsen@gmail.com"},
            new Contact{ Id = 1, FirstName = "Braydon", LastName = "Mcmillan", Email = "braydon71@icloud.com"},
            new Contact{ Id = 2, FirstName = "Eliot", LastName = "Page", Email = "contact@eliotpage.com" }
        };

        [HttpGet]
        public IActionResult GetAllContacts() => Ok(contacts);

        [HttpPost]
        public IActionResult AddContact([FromBody] Contact newContact)
        {
            contacts.Add(newContact);
            return CreatedAtRoute("GetSpecificContact", 
                new { index = contacts.IndexOf(newContact) }, newContact);
        }

        [HttpDelete]
        [Route("{personId}")]
        public IActionResult DeleteContact(int personId)
        {
            if ( contacts.RemoveAll(c => c.Id == personId) > 0)
                return NoContent();
            return BadRequest("Invalid index");
        }
        
        [HttpGet]
        [Route("findByName")]
        public IActionResult FindByName([FromBody] string nameFilter) 
            => Ok(ContactsController.contacts.Where(c => c.FirstName.Contains(nameFilter) || c.LastName.Contains(nameFilter)));

     }

    }
