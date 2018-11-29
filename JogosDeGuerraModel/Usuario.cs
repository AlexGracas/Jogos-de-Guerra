using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JogosDeGuerraModel
{
    [DataContract(IsReference = true)]
    public class Usuario
    {
        [DataMember]
        public int Id { get; set; }
        public IList<Batalha> Batalhas { get; set; }
        [DataMember]
        public String Name { get; set; }
        [DataMember]
        public String Email { get; set; }
    }
}
