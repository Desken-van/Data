using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        UsersContext db;
        public BaseController(UsersContext context)
        {
            db = context;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Base>>> Get()
        {
            return await db.Logins.ToListAsync();
     
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Base>> Get(int id)
        {
            Base user = await db.Logins.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<Base>> Post(Base user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Users;Trusted_Connection=True;";
            string sqlExpression = $"INSERT INTO logins (Login, Password, Role) VALUES ('{user.Login}', '{user.Password}', '{user.Role}')";
            //string sqlExpression = $"SET IDENTITY_INSERT logins ON";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                await db.SaveChangesAsync();
            }

           //db.Logins.Add(user);
           // await db.SaveChangesAsync();
            return Ok(user);
        }

        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<Base>> Put(Base user)
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
        public async Task<ActionResult<Base>> Delete(int id)
        {
            Base user = db.Logins.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            db.Logins.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
