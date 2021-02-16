using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Data.Models;

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
        public async Task<ActionResult<IEnumerable<Account>>> Get()
        {
            return await db.Logins.ToListAsync();   
        }
        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> Get(int id)
        {
            Account user = await db.Logins.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<Account>> Post(UserModel model)
        {
            Account user = new Account();
            user.Login = model.Login;
            user.Password = model.Password;
            user.Role = model.Role;
            if (user == null)
            {
                return BadRequest();
            }           
            db.Logins.Add(user);
           await db.SaveChangesAsync();
            return Ok(user);
        }

        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<Account>> Put(Account user)
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
        public async Task<ActionResult<Account>> Delete(int id)
        {
            Account user = db.Logins.FirstOrDefault(x => x.Id == id);
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
