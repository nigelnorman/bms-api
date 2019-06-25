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
    public class BusinessesController : ControllerBase
    {
        private readonly BmsDbContext db;
        private readonly IMapper mapper;
        public BusinessesController(BmsDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessViewModel>>> Get()
        {
            return await this.db.Businesses.Select(b => this.mapper.Map<BusinessViewModel>(b)).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessViewModel>> Get(int id)
        {
            var business = await this.db.Businesses.SingleOrDefaultAsync(b => b.Id == id);

            if (business == null)
            {
                throw new Exception("Business could not be found.");
            }

            return this.mapper.Map<BusinessViewModel>(business);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] BusinessViewModel businessVm)
        {
            this.db.Businesses.Add(new Business()
            {
                Name = businessVm.Name,
                DateFounded = businessVm.DateFounded,
            });

            await this.db.SaveChangesAsync();

            return this.Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BusinessViewModel>> Put(int id, [FromBody] BusinessViewModel businessVm)
        {
            var business = await this.db.Businesses.SingleOrDefaultAsync(b => b.Id == id);

            if (business == null)
            {
                throw new Exception("Business could not be found.");
            }

            this.db.Entry(business).CurrentValues.SetValues(businessVm);

            await this.db.SaveChangesAsync();

            return this.mapper.Map<BusinessViewModel>(await this.db.Businesses.SingleAsync(b => b.Id == id));

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var business = await this.db.Businesses.SingleOrDefaultAsync(b => b.Id == id);

            if (business == null)
            {
                throw new Exception("Business could not be found.");
            }

            this.db.Businesses.Remove(business);

            await this.db.SaveChangesAsync();

            return this.Ok();

        }

    }
}
