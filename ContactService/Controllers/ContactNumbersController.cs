using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ContactService.Models;

namespace ContactService.Controllers
{
    public class ContactNumbersController : ApiController
    {
        private ContactServiceContext db = new ContactServiceContext();

        // GET: api/ContactNumbers
        public IQueryable<ContactNumberDTO> GetContactNumbers()
        {
            var numbers = from b in db.ContactNumbers
                        select new ContactNumberDTO()
                        {
                            Id = b.Id,
                            Number = b.Number,
                            ContactPersonName = b.ContactPerson.Name,
                            Active = b.Active
                        };

            return numbers;
        }

        // GET: api/ContactNumbers/5
        [ResponseType(typeof(ContactNumber))]
        public async Task<IHttpActionResult> GetContactNumber(int id)
        {
            var contactNumber = await db.ContactNumbers.Include(b => b.ContactPerson).Select(b =>
                new ContactNumberDTO()
                {
                    Id = b.Id,
                    Number = b.Number,
                    ContactPersonName = b.ContactPerson.Name,
                    Active = b.Active
                }).SingleOrDefaultAsync(b => b.Id == id);
            if (contactNumber == null)
            {
                return NotFound();
            }

            return Ok(contactNumber);
        }

        // PUT: api/ContactNumbers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContactNumber(int id, ContactNumber contactNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contactNumber.Id)
            {
                return BadRequest();
            }

            db.Entry(contactNumber).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactNumberExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }        

        // POST: api/ContactNumbers
        [ResponseType(typeof(ContactNumber))]
        public async Task<IHttpActionResult> PostContactNumber(ContactNumber contactNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ContactNumbers.Add(contactNumber);
            await db.SaveChangesAsync();

            db.Entry(contactNumber).Reference(x => x.ContactPerson).Load();

            var dto = new ContactNumberDTO()
            {
                Id = contactNumber.Id,
                Number = contactNumber.Number,
                ContactPersonName = contactNumber.ContactPerson.Name
            };

            return CreatedAtRoute("DefaultApi", new { id = contactNumber.Id }, dto);
        }

        // DELETE: api/ContactNumbers/5
        [ResponseType(typeof(ContactNumber))]
        public async Task<IHttpActionResult> DeleteContactNumber(int id)
        {
            ContactNumber contactNumber = await db.ContactNumbers.FindAsync(id);
            if (contactNumber == null)
            {
                return NotFound();
            }

            db.ContactNumbers.Remove(contactNumber);
            await db.SaveChangesAsync();

            return Ok(contactNumber);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactNumberExists(int id)
        {
            return db.ContactNumbers.Count(e => e.Id == id) > 0;
        }
    }
}