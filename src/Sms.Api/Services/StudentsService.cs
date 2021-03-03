using Microsoft.EntityFrameworkCore;
using Sms.Data;
using Sms.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sms.Api.Services
{
    public class StudentsService
    {
        private readonly SmsDbContext db;

        public StudentsService(SmsDbContext db) 
        {
            this.db = db;
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await this.db.Students.ToListAsync();
        }

        public async Task<Student> GetById(int id)
        {
            var student = await this.db.Students.SingleOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                throw new Exception("Student could not be found.");
            }

            return student;
        }

        public async Task AddNew(Student newStudent)
        {
            if (!RequiredFieldsAreValid(newStudent))
                throw new Exception("Required input fields are missing.");

            this.db.Students.Add(newStudent);

            await this.db.SaveChangesAsync();
        }

        public async Task UpdateExisting(int id, Student student)
        {
            var existingStudent = await this.GetById(id);

            if (!RequiredFieldsAreValid(student))
                throw new Exception("Required input fields are missing");

            this.db.Entry(existingStudent).CurrentValues.SetValues(student);

            await this.db.SaveChangesAsync();
        }

        public async Task RemoveExisting(int id)
        {
            var existing = await this.GetById(id);

            this.db.Students.Remove(existing);

            await this.db.SaveChangesAsync();
        }

        private bool RequiredFieldsAreValid(Student student)
        {
            return !string.IsNullOrEmpty(student.FirstName)
                && !string.IsNullOrEmpty(student.LastName)
                && !string.IsNullOrEmpty(student.Email);
        }
    }
}
