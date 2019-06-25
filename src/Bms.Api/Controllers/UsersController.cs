using AutoMapper;
using Bms.Api.ViewModels;
using Bms.Data;
using Bms.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BmsDbContext db;
        private readonly IMapper mapper;
        public UsersController(BmsDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> Get()
        {
            return await this.db.Users.Select(u => this.mapper.Map<UserViewModel>(u)).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewModel>> Get(int id)
        {
            var user = await this.db.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new Exception("User could not be found.");
            }

            return this.mapper.Map<UserViewModel>(user);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserViewModel userVm)
        {
            this.db.Users.Add(new User()
            {
                FirstName = userVm.FirstName,
                LastName = userVm.LastName,
                Active = userVm.Active
            });

            await this.db.SaveChangesAsync();

            return this.Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserViewModel>> Put(int id, [FromBody] UserViewModel userVm)
        {
            var user = await this.db.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new Exception("User could not be found.");
            }

            this.db.Entry(user).CurrentValues.SetValues(userVm);

            await this.db.SaveChangesAsync();

            return this.mapper.Map<UserViewModel>(await this.db.Users.SingleAsync(u => u.Id == id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await this.db.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new Exception("User could not be found.");
            }

            this.db.Users.Remove(user);

            await this.db.SaveChangesAsync();

            return this.Ok();

        }

    }
}
