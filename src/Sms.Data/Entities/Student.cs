using System.ComponentModel.DataAnnotations;

namespace Sms.Data.Entities
{
    public class Student : EntityBase<int>
    {
        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string FavoriteBooksList { get; set; }
    }
}
