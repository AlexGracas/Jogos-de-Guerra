using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogosDeGuerraModel
{
    class Batalha
    {
        Tabuleiro Tabuleiro { get; set; }

        public IList<Exercito> Exercitos { get; set; }

        public Exercito Vencedor { get; set; } = null;
    }
}
