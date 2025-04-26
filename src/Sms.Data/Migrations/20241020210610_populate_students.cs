using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Sms.Data.Entities;

namespace Sms.Data.Migrations
{
    public partial class populate_students : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[]
                {
                    nameof(Student.Id),
                    nameof(Student.FirstName),
                    nameof(Student.LastName),
                    nameof(Student.Email),
                    nameof(Student.DateCreated)
                },
                columnTypes: new[]
                {
                    "int",
                    "nvarchar(max)",
                    "nvarchar(max)",
                    "nvarchar(max))",
                    "datetime2",
                },
                values: new object[]
                {
                    1,
                    "Bob",
                    "Ross",
                    "bobross@art.net",
                    DateTime.Parse("07/20/2021 21:40:55"),
                });
            
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[]
                {
                    nameof(Student.Id),
                    nameof(Student.FirstName),
                    nameof(Student.LastName),
                    nameof(Student.Email),
                    nameof(Student.DateCreated)
                },
                columnTypes: new[]
                {
                    "int",
                    "nvarchar(max)",
                    "nvarchar(max)",
                    "nvarchar(max))",
                    "datetime2",
                },
                values: new object[]
                {
                    2,
                    "Douglas",
                    "Adams",
                    "douglasadams@universe.org",
                    DateTime.Parse("07/20/2022 21:40:55"),
                });
            
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[]
                {
                    nameof(Student.Id),
                    nameof(Student.FirstName),
                    nameof(Student.LastName),
                    nameof(Student.Email),
                    nameof(Student.DateCreated)
                },
                columnTypes: new[]
                {
                    "int",
                    "nvarchar(max)",
                    "nvarchar(max)",
                    "nvarchar(max))",
                    "datetime2",
                },
                values: new object[]
                {
                    3,
                    "Annie",
                    "Clark",
                    "stvincent@music.com",
                    DateTime.Parse("07/20/2023 21:40:55"),
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[Students]");
        }
    }
}
