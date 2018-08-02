using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string UserName { get; set; }


        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }
    }
    //[DataContract]
    //public enum Status
    //{
    //    online,
    //    offline
    //}
}
