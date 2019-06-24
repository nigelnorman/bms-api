using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Bms.Api.ViewModels
{
    [DataContract]
    public class BusinessViewModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime DateFounded { get; set; }

    }
}
