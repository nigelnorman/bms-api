using System.Runtime.Serialization;

namespace Sms.Api.ViewModels
{
    [DataContract]
    public class BookViewModel
    {
        [DataMember]
        public string Title { get; set; }
    }
}
