using AutoMapper;
using Sms.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sms.Data;
using Sms.Data.Entities;
using Sms.Api.Services;

namespace Sms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly StudentsService studentsService;
        public StudentsController(IMapper mapper, StudentsService studentsService)
        {
            this.mapper = mapper;
            this.studentsService = studentsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentViewModel>>> Get()
        {
            try
            {
                return (await this.studentsService.GetAll())
                    .ToList()
                    .Select(u => this.mapper.Map<StudentViewModel>(u))
                    .ToList();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentViewModel>> GetById(int id)
        {
            try
            {
                var student = await this.studentsService.GetById(id);

                return this.mapper.Map<StudentViewModel>(student);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] StudentViewModel studentVm)
        {
            try
            {
                var newStudent = this.mapper.Map<Student>(studentVm);

                await this.studentsService.AddNew(newStudent);

                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);   
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StudentViewModel>> Put(int id, [FromBody] StudentViewModel studentVm)
        {
            try
            {
                var updatedStudent = this.mapper.Map<Student>(studentVm);

                await this.studentsService.UpdateExisting(id, updatedStudent);

                return this.mapper.Map<StudentViewModel>(await this.studentsService.GetById(id));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await this.studentsService.RemoveExisting(id);

                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
