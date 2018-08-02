using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Content { get; set; }
    }
}
