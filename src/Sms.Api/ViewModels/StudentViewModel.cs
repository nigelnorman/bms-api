using Sms.Data.Entities;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Sms.Api.ViewModels
{
    [DataContract]
    public class StudentViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public ICollection<BookViewModel> FavoriteBooksList { get; set; } = new List<BookViewModel>();
    }
}
