using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogosDeGuerraModel
{
    public class Usuario
    {
        public int Id { get; set; }
        public IList<Batalha> Batalhas { get; set; }

        public String Name { get; set; }

        public String Email { get; set; }
    }
}
