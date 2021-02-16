using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMSController : ControllerBase
    {

        UsersContext db;
        public SMSController(UsersContext context)
        {
            db = context;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SMS>>> Get()
        {
            return await db.Sms.ToListAsync();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SMS>> Get(int id)
        {
            SMS user = await db.Sms.FirstOrDefaultAsync(x => x.Sender == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<Account>> Post(SMSModel model)
        {
            SMS sms = new SMS();
            sms.Sender = model.Sender;
            sms.Sms = model.Sms;
            sms.Recipient = model.Recipient;

            if (sms == null)
            {
                return BadRequest();
            }
            db.Sms.Add(sms);
            await db.SaveChangesAsync();
            return Ok(sms);
        }

        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<SMS>> Put(SMS user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!db.Logins.Any(x => x.Id == user.Id))
            {
                return NotFound();
            }

            db.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SMS>> Delete(int id)
        {
            SMS user = db.Sms.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            db.Sms.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
        ////////////////////////
    }
}
