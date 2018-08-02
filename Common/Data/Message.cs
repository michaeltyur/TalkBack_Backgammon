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
    public class Message
    {
        [Key]
        [DataMember]
        public int MessageId { get; set; }

        [DataMember]
        public string UserName
        {
            get { return _userName; }
            set { if (value.Length < 15) _userName = value; }
        }
        private string _userName;

        [DataMember]
        public string Content
        {
            get { return _content; }
            set { if (value.Length < 100) _content = value; }
        }
        private string _content;

        //public int UserId { get; set; }
        //public virtual User User { get; set; }

        //[DataMember]
        //public int RecipientId { get; set; }
        //[DataMember]
        //public virtual User Recipient { get; set; }

        [DataMember]
        public DateTime Date { get; set; }


    }
}
