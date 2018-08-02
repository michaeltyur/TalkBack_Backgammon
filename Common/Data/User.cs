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
        [Key]
        [DataMember]
        public int UserId { get; set; }

        private string _userName;
        [DataMember]
        public string UserName
        {
            get { return _userName; }

            set { if (value.Length < 11 && value.Length > 2) _userName = value; }
        }

        private string _password;
        [DataMember]
        public string Password
        {
            get { return _password; }

            set { if (value.Length < 11 && value.Length > 2) _password = value; }
        }

        private string _firstName;
        [DataMember]
        public string FirstName
        {
            get { return _firstName; }

            set { if (value != null && value.Length < 16 && value.Length > 2) _firstName = value; }
        }

        private string _lastName;
        [DataMember]
        public string LastName
        {
            get { return _lastName; }

            set { if (value!=null && value.Length < 16 && value.Length > 2) _lastName = value; }
        }
        //[DataMember]
        //public virtual ICollection<Message> Messages { get; set; }

    }

}
