using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Common.Data
{
    [DataContract]
    public class GameColors
    {
        [DataMember]
        public Brush Selected ;
        [DataMember]
        public Brush Black ;
        [DataMember]
        public Brush White ;

                
    }
}
