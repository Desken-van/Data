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
            return await db.sMs.ToListAsync();

        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SMS>> Get(int id)
        {
            SMS user = await db.sMs.FirstOrDefaultAsync(x => x.Sender == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<SMS>> Post(SMS user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Users;Trusted_Connection=True;";
            string sqlExpression = $"INSERT INTO sms (Sender, SMS, Recipient,Number) VALUES ('{user.Sender}', '{user.Sms}', '{user.Recipient}','{user.Number}')";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                await db.SaveChangesAsync();
            }

            //db.sMs.Add(user);
            // await db.SaveChangesAsync();

            return Ok(user);
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
            SMS user = db.sMs.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            db.sMs.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
        ////////////////////////
    }
}
