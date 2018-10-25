using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogosDeGuerraModel
{
    class Tabuleiro
    {
        public int Largura { get; set; }

        public int Altura { get; set; }

        public IEnumerable<Posicao> ElementosDoExercito { get; set; }

    }
}
